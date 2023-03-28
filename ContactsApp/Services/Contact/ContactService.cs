using ContactsApp.Models;
using ContactsApp.Repository;

namespace ContactsApp.Services;

public class ContactService: IContactService
{
    private readonly IContactRepository _repository;

    public ContactService(IContactRepository repository)
    {
        _repository = repository;
    }
    
    public IAsyncEnumerable<Contact> GetContactsAsync()
    {
        return _repository.GetContactsAsync();
    }

    public Contact GetContactById(int contactId)
    {
        var contact = _repository.GetContactById(contactId);
        if (contact == null) throw new ContactNotFoundException(contactId);
        
        return contact;
    }

    public Contact CreateContact(Contact contact)
    {
        var newContact = _repository.CreateContact(contact);
        if (newContact == null) throw new Exception();
        return newContact;
    }

    public void DeleteContact(int contactId)
    {
        var contact = _repository.GetContactById(contactId);
        if (contact == null) throw new ContactNotFoundException(contactId);
        
        _repository.DeleteContact(contact);
    }

    public void UpdateContact(Contact contact)
    {
        _repository.UpdateContact(contact);
    }
}