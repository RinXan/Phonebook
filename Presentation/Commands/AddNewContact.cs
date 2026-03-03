using Phonebook.Application;
using Phonebook.Domain;

namespace Phonebook.Presentation.Commands
{
    public class AddNewContact : ICommand
    {
        private readonly ContactService _contactService;
        public string Name => "Add new contact";
        public AddNewContact(ContactService contactService)
        {
            _contactService = contactService;
        }
        public void Execute()
        {
            ConsoleHelper.WriteHeader("### Add new contact ###\n");

            Contact contact = new Contact();
            Console.Write("Name: ");
            contact.Name = Console.ReadLine();
            Console.Write("Phone: ");
            contact.Phone = Console.ReadLine();
            Console.Write("Email: ");
            contact.Email = Console.ReadLine();
            Console.Write("Group: ");
            contact.Group = Console.ReadLine();

            try
            {
                _contactService.AddContact(contact);
                ConsoleHelper.WriteSuccess("Contact added succesfully");
            }
            catch (ArgumentException ex)
            {
                ConsoleHelper.WriteError($"Error: {ex.Message}");
            }
        }
    }
}
