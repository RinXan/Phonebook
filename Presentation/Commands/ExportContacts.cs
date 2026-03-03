using System.Text;
using Phonebook.Application;

namespace Phonebook.Presentation.Commands
{
    public class ExportContacts : ICommand
    {
        private readonly ContactService _contactService;
        public string Name => "Export contacts";
        public ExportContacts(ContactService contactService) 
        {
            _contactService = contactService;
        }
        public void Execute()
        {
            ConsoleHelper.WriteHeader("### Export to CSV");

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
        }
    }
}
