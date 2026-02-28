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

        public void AddContact(Contact contact) 
        {
            if (string.IsNullOrWhiteSpace(contact.Name)) throw new ArgumentException("Name cannot be empty");
            
            _repository.Add(contact);
            //_repository.SaveChanges();
        }
    }
}
