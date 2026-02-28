using Phonebook.Application;
using Phonebook.Domain;

namespace Phonebook.Presentation
{
    public class PhoneBookApp
    {
        private readonly ContactService _contactService;

        public PhoneBookApp(ContactService contactService)
        {
            _contactService = contactService;
        }

        public void Run()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("### PhoneBook ###");
                Console.WriteLine("1. Show all contacts");
                Console.WriteLine("2. Add new contact");
                Console.WriteLine("0. Exit");

                Console.Write("Choice: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Goob by");
                        return;
                    case 1:
                        ShowAllContacts();
                        break;
                    case 2:
                        AddNewContact();
                        break;
                    default:
                        Console.WriteLine("Not available command");
                        break;
                }
                Console.WriteLine("\nPress any key...");
                Console.ReadKey();
            }
        }

        private void ShowAllContacts()
        {
            Console.Clear();
            Console.WriteLine("### All contacts ###");

            List<Contact> contacts = _contactService.GetAllContacts().ToList();

            if (contacts.Count == 0)
            {
                Console.WriteLine("\nPhone book is empty");
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
        
        private void AddNewContact()
        {
            Console.Clear();
            Console.WriteLine("### Add new contact ###\n");

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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Contact added succesfully");
                Console.ResetColor();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
