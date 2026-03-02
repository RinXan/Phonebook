using System.Text.Json;
using Phonebook.Domain;

namespace Phonebook.Infrastructure
{
    public class JsonContactRepository : IContactRepository
    {
        private readonly string _filePath;
        private List<Contact> _contacts;
        private int _nextId;

        public JsonContactRepository(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath)) throw new ArgumentException("File path is not correct");
            _filePath = filePath;
            Load();
        }

        private void Load()
        {
            if (File.Exists(_filePath))
            {
                try
                {
                    var json = File.ReadAllText(_filePath);
                    _contacts = JsonSerializer.Deserialize<List<Contact>>(json);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("[File does not exist]");
                }
            } else
            {
                _contacts = new List<Contact>(); 
            }
            _nextId = _contacts.Any() ? _contacts.Max(c => c.Id) + 1 : 1;
        }

        public void Add(Contact contact)
        {
            contact.Id = _nextId++;
            _contacts.Add(contact);
        }

        public void Delete(int id)
        {
            var contact = GetById(id);
            if (contact != null) 
            {
                _contacts.Remove(contact);
            }
        }

        public IEnumerable<Contact> GetAll()
        {
            return _contacts.ToList();
        }

        public Contact? GetById(int id)
        {
            return _contacts.FirstOrDefault(c => c.Id == id);
        }

        public void SaveChanges()
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var json = JsonSerializer.Serialize(_contacts, options);
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                throw new IOException("Error while saving changes to file", ex);
            }
        }

        public void Update(Contact contact)
        {
            var existing = GetById(contact.Id);
            if (existing != null)
            {
                existing.Name = contact.Name;
                existing.Phone = contact.Phone;
                existing.Email = contact.Email;
                existing.Group = contact.Group;
            }

        }
    }
}
