using Phonebook.Application;
using Phonebook.Domain;

namespace Phonebook.Presentation.Commands
{
    public class ShowContactsByGroup : ICommand
    {
        private readonly ContactService _contactService;
        public string Name => "Show contacts by group";
        public ShowContactsByGroup(ContactService contactService)
        {
            _contactService = contactService;
        }
        public void Execute()
        {
            ConsoleHelper.WriteHeader("### Contacts by group ###");

            List<string> groups = _contactService.GetAllGroups().ToList();

            if (!groups.Any())
            {
                Console.WriteLine("Contacts are without groups");
                return;
            }

            Console.WriteLine("Available groups:");

            for (int i = 0; i < groups.Count; i++)
            {
                Console.WriteLine($"{i + 1}) {groups[i]}");
            }

            Console.Write("Choose group number: ");

            if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > groups.Count)
            {
                ConsoleHelper.WriteInfo("Not correct choice");
                return;
            }

            string selectedGroup = groups[choice - 1];
            List<Contact> contactsFromGroup = _contactService.GetContactsByGroup(selectedGroup).ToList();

            if (!contactsFromGroup.Any())
            {
                ConsoleHelper.WriteInfo("Contacts in this group does not exist");
            }
            else
            {
                Console.WriteLine("ID |NAME\t|PHONE\t|EMAIL\t|GROUP\t");
                Console.WriteLine(new string('-', 45));

                foreach (Contact contact in contactsFromGroup)
                {
                    Console.WriteLine($"{contact.Id}  |{contact.Name}\t|{contact.Phone}\t|{contact.Email}\t|{contact.Group}");
                }

                Console.WriteLine(new string('-', 45));
            }
        }
    }
}
