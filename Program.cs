using Phonebook.Application;
using Phonebook.Domain;
using Phonebook.Infrastructure;
using Phonebook.Presentation;

namespace Phonebook
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IContactRepository repository = new JsonContactRepository("contacts.json");
            ContactService contactService = new ContactService(repository);
            PhoneBookApp app = new PhoneBookApp(contactService);
            
            app.Run();
        }
    }
}
