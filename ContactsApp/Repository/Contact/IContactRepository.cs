using ContactsApp.Models;

namespace ContactsApp.Repository;

public interface IContactRepository
{
    IAsyncEnumerable<Contact> GetContactsAsync();
    Contact? GetContactById(int contactId);
    Contact? CreateContact(Contact contact);
    void DeleteContact(Contact contact);
    void UpdateContact(Contact contact);
}