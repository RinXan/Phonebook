using Phonebook.Application;
using Phonebook.Domain;

namespace Phonebook.Presentation.Commands
{
    public class ShowAllContacts : ICommand
    {
        private readonly ContactService _contactService;
        public string Name => "Show all contacts";
        public ShowAllContacts(ContactService contactService) 
        {
            _contactService = contactService;
        }

        public void Execute() 
        {
            ConsoleHelper.WriteHeader("### All contacts ###");

            List<Contact> contacts = _contactService.GetAllContacts().ToList();

            if (contacts.Count == 0)
            {
                ConsoleHelper.WriteInfo("\nPhone book is empty");
            }
            else
            {
                foreach (Contact contact in contacts)
                {
                    Console.WriteLine($"ID: {contact.Id}");
                    Console.WriteLine($"Name: {contact.Name}");
                    Console.WriteLine($"Phone: {contact.Phone}");
                    Console.WriteLine($"Email: {contact.Email}");
                    Console.WriteLine($"Group: {contact.Group}");
                    Console.WriteLine(new string('-', 20));
                }
            }
        }
    }
}
