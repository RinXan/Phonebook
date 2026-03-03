using System.Text;
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
                Console.WriteLine("6. Show contacts by a group");
                Console.WriteLine("7. Export to CSV");
                Console.WriteLine("0. Exit");

                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    ConsoleHelper.WriteError("Choice is not correct. Press any key...");
                    Console.ReadKey();
                    continue;
                }

                switch (choice)
                {
                    case 0:
                        ConsoleHelper.WriteSuccess("\nGoob by");
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
                    case 6:
                        ShowContactsByGroup();
                        break;
                    case 7:
                        ExportContacts();
                        break;
                    default:
                        ConsoleHelper.WriteInfo("Not available command");
                        break;
                }
                ConsoleHelper.WriteInfo("\nPress any key...");
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
                ConsoleHelper.WriteSuccess("Contact added succesfully");
            }
            catch (ArgumentException ex)
            {
                ConsoleHelper.WriteError($"Error: {ex.Message}");
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
        private void UpdateContact()
        {
            Console.Clear();
            Console.WriteLine("### Update contact ###");

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
        private void SearchContact()
        {
            Console.Clear();
            Console.WriteLine("### Search contact ###");
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
        private void ExportContacts()
        {
            Console.Clear();
            Console.WriteLine("### Export to CSV");

            var csvData = _contactService.ExportToCsv();
            if (string.IsNullOrEmpty(csvData))
            {
                ConsoleHelper.WriteInfo("Contact's list is empty");
                return;
            }

            Console.Write("Enter file name (defualt name contacts.csv): ");
            var fileName = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(fileName))
                fileName = "contacts.csv";
            else if (!fileName.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
                fileName += ".csv";

            try
            {
                File.WriteAllText(fileName, csvData, Encoding.UTF8);
                ConsoleHelper.WriteSuccess($"Contacts exported to {fileName}");
            }
            catch (Exception ex)
            {
                ConsoleHelper.WriteError($"Error while exporting contacts: {ex.Message}");
            }

            Console.ReadKey();
        }
        private void ShowContactsByGroup()
        {
            Console.Clear();
            Console.WriteLine("### Contacts by group ###");

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
        private string InputWithDefault(string fieldName, string currentValue)
        {
            Console.Write($"{fieldName} [{currentValue}]: ");
            string input = Console.ReadLine();
            return string.IsNullOrWhiteSpace(input) ? currentValue : input;
        }
    }
}
