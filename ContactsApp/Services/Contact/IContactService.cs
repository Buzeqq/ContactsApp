using ContactsApp.Models;

namespace ContactsApp.Services;

public interface IContactService
{
    IAsyncEnumerable<Contact> GetContactsAsync();
    Contact GetContactById(int contactId);
    Contact CreateContact(Contact contact);
    void DeleteContact(int contactId);
    void UpdateContact(Contact contact);
}