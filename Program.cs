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
            IContactRepository repository = new InMemoryRepository();
            ContactService contactService = new ContactService(repository);
            PhoneBookApp app = new PhoneBookApp(contactService);
            
            app.Run();
        }
    }
}
