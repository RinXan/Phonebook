using System;
using System.Collections.Generic;
using System.Text;
using Phonebook.Application;
using Phonebook.Domain;

namespace Phonebook.Presentation.Commands
{
    public class DeleteContact : ICommand
    {
        private readonly ContactService _contactService;
        public string Name => "Delete contact";
        public DeleteContact(ContactService contactService) 
        {
            _contactService = contactService;
        }
        public void Execute()
        {
            ConsoleHelper.WriteHeader("### Delete contact ###");

            List<Contact> contacts = _contactService.GetAllContacts().ToList();

            if (!contacts.Any())
            {
                Console.WriteLine("Phone book is empty");
                return;
            }

            Console.WriteLine("NAME\t|ID");
            Console.WriteLine(new string('-', 25));

            foreach (Contact contact in contacts)
            {
                Console.WriteLine($"{contact.Name}\t|{contact.Id}");
            }

            Console.WriteLine(new string('-', 25));
            Console.Write("Enter contact ID: ");

            if (!int.TryParse(Console.ReadLine(), out int contactId))
            {
                ConsoleHelper.WriteInfo("ID is not correct");
                return;
            }

            Contact existing = _contactService.GetContactById(contactId);
            if (existing == null)
            {
                ConsoleHelper.WriteInfo($"Contact with id {contactId} does not exist");
                return;
            }

            try
            {
                _contactService.DeleteContact(contactId);
                ConsoleHelper.WriteSuccess($"\nContact {existing.Name} deleted!");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Error while deleting {existing.Name}");
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
