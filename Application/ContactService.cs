using System;
using System.Collections.Generic;
using System.Text;
using Phonebook.Domain;

namespace Phonebook.Application
{
    public class ContactService
    {
        private readonly IContactRepository _repository;
        public ContactService(IContactRepository repository)
        {
            if (repository == null) throw new ArgumentException("Repository is null");
            _repository = repository;
        }
        public IEnumerable<Contact> GetAllContacts() => _repository.GetAll();
        public Contact GetContactById(int id) => _repository.GetById(id);
        public void AddContact(Contact contact) 
        {
            if (string.IsNullOrWhiteSpace(contact.Name)) throw new ArgumentException("Name cannot be empty");
            
            _repository.Add(contact);
            _repository.SaveChanges();
        }
        public void DeleteContact(int contactId)
        {
            if (contactId <= 0) throw new ArgumentException("Contact id is not correct");
            _repository.Delete(contactId);
            _repository.SaveChanges();
        }
        public void UpdateContact(Contact contact)
        {
            if (contact.Id <= 0) throw new ArgumentException("Contact id is not correct");
            _repository.Update(contact);
            _repository.SaveChanges();
        }   
        public IEnumerable<Contact> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm)) return Enumerable.Empty<Contact>();

            searchTerm = searchTerm.Trim().ToLowerInvariant();

            return _repository.GetAll().Where(c =>
                c.Name.ToLowerInvariant().Contains(searchTerm) ||
                c.Phone.ToLowerInvariant().Contains(searchTerm) ||
                c.Email.ToLowerInvariant().Contains(searchTerm));
        }
        public IEnumerable<string> GetAllGroups()
        {
            return _repository.GetAll()
                .Select(c => c.Group)
                .Where(g => !string.IsNullOrWhiteSpace(g))
                .Distinct()
                .OrderBy(g => g);
        }
        public IEnumerable<Contact> GetContactsByGroup(string group)
        {
            if (string.IsNullOrWhiteSpace(group)) return Enumerable.Empty<Contact>();

            return _repository.GetAll().Where(c => c.Group.Equals(group, StringComparison.OrdinalIgnoreCase));
        }
    }
}
