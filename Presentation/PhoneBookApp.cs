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
                Console.WriteLine("3. Delete contact");
                Console.WriteLine("4. Update contact");
                Console.WriteLine("0. Exit");

                Console.Write("Choice: ");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("\nGoob by");
                        return;
                    case 1:
                        ShowAllContacts();
                        break;
                    case 2:
                        AddNewContact();
                        break;
                    case 3:
                        DeleteContact();
                        break;
                    case 4:
                        UpdateContact();
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
        
        private void DeleteContact()
        {
            Console.Clear();
            Console.WriteLine("### Delete contact ###");

            List<Contact> contacts = _contactService.GetAllContacts().ToList();

            Console.WriteLine("NAME\t|\tID");
            Console.WriteLine(new string('-', 25));

            int i = 1;
            
            foreach (Contact contact in contacts)
            {
                Console.WriteLine($"{contact.Name}\t|\t{contact.Id}");
                i++;
            }
            
            Console.WriteLine(new string('-', 25));
            Console.Write("Enter contact ID: ");
            
            int contactId = int.Parse(Console.ReadLine());

            _contactService.DeleteContact(contactId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nContact with id {contactId} deleted!");
            Console.ResetColor();
        }
        private void UpdateContact()
        {
            Console.WriteLine("Not implemented yet.\nPress any key...");
            Console.ReadKey();
        }
    }
}
