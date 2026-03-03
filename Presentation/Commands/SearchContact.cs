using Phonebook.Application;
using Phonebook.Domain;

namespace Phonebook.Presentation.Commands
{
    public class SearchContact : ICommand
    {
        private readonly ContactService _contactService;
        public string Name => "Search contact";
        public SearchContact(ContactService contactService) 
        {
            _contactService = contactService;
        }
        public void Execute()
        {
            ConsoleHelper.WriteHeader("### Search contact ###\n");
            Console.Write("Enter text for searching (name, email, phone): ");

            string term = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(term))
            {
                ConsoleHelper.WriteInfo("Serach text cannot be empty");
                return;
            }

            List<Contact> results = _contactService.Search(term).ToList();

            if (!results.Any())
            {
                ConsoleHelper.WriteInfo($"Contacts not found. Search term: {term}");
            }
            else
            {
                Console.WriteLine($"Count of found contacts: {results.Count}");
                Console.WriteLine("\nID |NAME\t|PHONE\t|EMAIL\t|GROUP\t");
                Console.WriteLine(new string('-', 45));

                foreach (Contact contact in results)
                {
                    Console.WriteLine($"{contact.Id}  |{contact.Name}\t|{contact.Phone}\t|{contact.Email}\t|{contact.Group}");
                }

                Console.WriteLine(new string('-', 45));
            }
        }
    }
}
