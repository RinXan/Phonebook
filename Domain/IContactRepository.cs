namespace Phonebook.Domain
{
    public interface IContactRepository
    {
        void Add(Contact contact);
        Contact? GetById(int id);
        IEnumerable<Contact> GetAll();
        void Delete(int id);
        void Update(Contact contact); 
        void SaveChanges();
    }
}
