using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Phonebook.Domain;

namespace Phonebook.Infrastructure
{
    public class InMemoryRepository : IContactRepository
    {
        private readonly List<Contact> _contacts = new();
        private int _nextId = 1;
        public void AddContact(Contact contact)
        {
            contact.Id = _nextId++;
            _contacts.Add(contact);
        }

        public void DeleteContact(int id)
        {
            Contact contact = GetContact(id);
            if (contact != null) _contacts.Remove(contact);
        }

        public IEnumerable<Contact> GetAllContacts() => _contacts;

        public Contact? GetContact(int id) => _contacts.FirstOrDefault(c => c.Id == id);

        public void SaveChanges()
        {
            Console.WriteLine("Not implemented yet\nPress any key...");
            Console.ReadKey();
        }

        public void UpdateContact(Contact contact)
        {
            Contact updating = GetContact(contact.Id);

            if (updating != null)
            {
                updating.Name = contact.Name;
                updating.Email = contact.Email;
                updating.PhoneNumber = contact.PhoneNumber;
                updating.Group = contact.Group;
            }
        }
    }
}
