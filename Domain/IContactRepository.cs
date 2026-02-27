using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Domain
{
    public interface IContactRepository
    {
        void AddContact(Contact contact);
        Contact? GetContact(int id);
        IEnumerable<Contact> GetAllContacts();
        void DeleteContact(int id);
        void UpdateContact(Contact contact); 
        void SaveChanges();
    }
}
