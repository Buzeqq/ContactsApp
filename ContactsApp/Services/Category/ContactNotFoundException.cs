namespace ContactsApp.Services;

public class ContactNotFoundException: Exception
{
    public ContactNotFoundException(int contactId):
        base($"Contact not found: {contactId}") { }
}