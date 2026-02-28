using Phonebook.Domain;

namespace Phonebook.Infrastructure
{
    public class InMemoryRepository : IContactRepository
    {
        private readonly List<Contact> _contacts = new();
        private int _nextId = 1;
        public void Add(Contact contact)
        {
            contact.Id = _nextId++;
            _contacts.Add(contact);
        }

        public void Delete(int id)
        {
            Contact contact = GetById(id);
            if (contact != null) _contacts.Remove(contact);
        }

        public IEnumerable<Contact> GetAll() => _contacts.ToList();

        public Contact? GetById(int id) => _contacts.FirstOrDefault(c => c.Id == id);

        public void SaveChanges()
        {
            Console.WriteLine("Not implemented yet\nPress any key...");
            Console.ReadKey();
        }

        public void Update(Contact contact)
        {
            Contact updating = GetById(contact.Id);

            if (updating != null)
            {
                updating.Name = contact.Name;
                updating.Email = contact.Email;
                updating.Phone = contact.Phone;
                updating.Group = contact.Group;
            }
        }
    }
}
