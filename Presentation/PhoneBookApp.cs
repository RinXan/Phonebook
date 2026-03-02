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
                Console.WriteLine("3. Search contact");
                Console.WriteLine("4. Delete contact");
                Console.WriteLine("5. Update contact");
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
                        SearchContact();
                        break;
                    case 4:
                        DeleteContact();
                        break;
                    case 5:
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
                Console.WriteLine("ID is not correct");
                return;
            }

            Contact existing = _contactService.GetContactById(contactId);
            if (existing == null)
            {
                Console.WriteLine($"Contact with id {contactId} does not exist");
                return;
            }

            try
            {
                _contactService.DeleteContact(contactId);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nContact {existing.Name} deleted!");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while deleting {existing.Name}");
                Console.WriteLine(ex.ToString());
            }
            Console.ResetColor();
        }
        private void UpdateContact()
        {
            Console.Clear();
            Console.WriteLine("### Update contact ###");

            List<Contact> contacts = _contactService.GetAllContacts().ToList();

            if (!contacts.Any())
            {
                Console.WriteLine("Phone book is empty");
                return;
            }

            Console.WriteLine("NAME\t|ID\t|EMAIL\t|GROUP");
            Console.WriteLine(new string('-', 45));

            foreach (Contact contact in contacts)
            {
                Console.WriteLine($"{contact.Name}\t|{contact.Id}\t|{contact.Email}\t|{contact.Group}");
            }

            Console.WriteLine(new string('-', 45));
            Console.Write("Enter contact ID: ");

            if (!int.TryParse(Console.ReadLine(), out int contactId))
            {
                Console.WriteLine("ID is not correct");
                return;
            }

            Contact existing = _contactService.GetContactById(contactId);
            if (existing == null)
            {
                Console.WriteLine($"Contact with id {contactId} does not exist");
                return;
            }

            Contact updated = new Contact
            {
                Id = existing.Id,
                Name = InputWithDefault("Name", existing.Name),
                Email = InputWithDefault("Email", existing.Email),
                Group = InputWithDefault("Group", existing.Group)
            };

            try
            {
                _contactService.UpdateContact(updated);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"Contact {updated.Name} updated");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Error while updating {updated.Name}");
                Console.WriteLine(ex.ToString());
            }
            Console.ResetColor();
        }
        private void SearchContact()
        {
            Console.WriteLine("Not implemented yet");
        }
        private string InputWithDefault(string fieldName, string currentValue)
        {
            Console.Write($"{fieldName} [{currentValue}]: ");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? currentValue : input;
        }
    }
}
