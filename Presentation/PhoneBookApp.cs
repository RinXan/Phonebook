using Phonebook.Application;
using Phonebook.Presentation.Commands;

namespace Phonebook.Presentation
{
    public class PhoneBookApp
    {
        private readonly ContactService _contactService;
        private readonly List<ICommand> _commands;
        public PhoneBookApp(ContactService contactService)
        {
            _contactService = contactService;
            _commands = new List<ICommand>() 
            {
                new ShowAllContacts(_contactService),
                new ShowContactsByGroup(_contactService),
                new AddNewContact(_contactService),
                new DeleteContact(_contactService),
                new UpdateContact(_contactService),
                new SearchContact(_contactService),
                new ExportContacts(_contactService),
            };
        }
        public void Run()
        {
            while (true)
            {
                ConsoleHelper.WriteHeader("### PhoneBook ###");

                for (int i = 0; i < _commands.Count(); i++)
                {
                    ConsoleHelper.Write($"{i + 1}) {_commands[i].Name}");
                }

                ConsoleHelper.Write("0) Exit");
                Console.Write("Choice: ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    ConsoleHelper.WriteError("Choice is not correct. Press any key...");
                    Console.ReadKey();
                    continue;
                }

                if (choice == 0)
                {
                    ConsoleHelper.WriteSuccess("Good By");
                    return;
                }

                if (choice < 0 || choice > _commands.Count) 
                {
                    ConsoleHelper.WriteInfo("Unavailable command");
                    Console.ReadKey();
                    continue;
                }

                try
                {
                    _commands[choice - 1].Execute();
                }
                catch (Exception ex)
                {
                    ConsoleHelper.WriteError($"Error message: {ex.Message}");
                }
                ConsoleHelper.WriteInfo("\nPress any key...");
                Console.ReadKey();
            }
        }
    }
}