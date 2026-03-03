using Phonebook.Application;
using Phonebook.Domain;

namespace Phonebook.Presentation.Commands
{
    public class UpdateContact : ICommand
    {
        private readonly ContactService _contactService;
        public string Name => "Update contact";
        public UpdateContact(ContactService contactService) 
        {
            _contactService = contactService;
        }
        public void Execute()
        {
            ConsoleHelper.WriteHeader("### Update contact ###");

            List<Contact> contacts = _contactService.GetAllContacts().ToList();

            if (!contacts.Any())
            {
                ConsoleHelper.WriteInfo("Phone book is empty");
                return;
            }

            Console.WriteLine("ID |NAME\t|PHONE\t|EMAIL\t|GROUP\t");
            Console.WriteLine(new string('-', 45));

            foreach (Contact contact in contacts)
            {
                Console.WriteLine($"{contact.Id}  |{contact.Name}\t|{contact.Phone}\t|{contact.Email}\t|{contact.Group}");
            }

            Console.WriteLine(new string('-', 45));
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

            Contact updated = new Contact
            {
                Id = existing.Id,
                Name = InputWithDefault("Name", existing.Name),
                Phone = InputWithDefault("Phone", existing.Phone),
                Email = InputWithDefault("Email", existing.Email),
                Group = InputWithDefault("Group", existing.Group)
            };

            try
            {
                _contactService.UpdateContact(updated);
                ConsoleHelper.WriteSuccess($"Contact {updated.Name} updated");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Error while updating {updated.Name}");
                Console.WriteLine(ex.ToString());
            }
        }
        private string InputWithDefault(string fieldName, string currentValue)
        {
            Console.Write($"{fieldName} [{currentValue}]: ");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? currentValue : input;
        }

    }
}
