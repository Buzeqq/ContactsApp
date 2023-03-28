using ContactsApp.Models;
using DbContext = ContactsApp.Database.DbContext;

namespace ContactsApp.Repository;

public sealed class ContactRepository : IContactRepository, IDisposable
{
    private readonly DbContext _dbContext;

    public ContactRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IAsyncEnumerable<Contact> GetContactsAsync()
    {
        return _dbContext.Contacts.AsAsyncEnumerable();
    }

    public Contact? GetContactById(int contactId)
    {
        return _dbContext.Contacts
            .Where(contact => contact.Id == contactId)
            .Select(contact => new Contact
            {
                Id = contact.Id,
                Name = contact.Name, 
                Surname = contact.Surname, 
                Email = contact.Email, 
                Category = contact.Category,
                BirthDay = contact.BirthDay,
                PhoneNumber = contact.PhoneNumber
            })
            .FirstOrDefault();
    }

    public Contact? CreateContact(Contact contact)
    {
        var newContact = _dbContext.Contacts.Add(contact);
        _dbContext.SaveChanges();

        return newContact.Entity;
    }

    public void DeleteContact(Contact contact)
    {
        _dbContext.Contacts.Remove(contact);
        _dbContext.SaveChanges();
    }

    public void UpdateContact(Contact contact)
    {
        _dbContext.Contacts.Update(contact);
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}