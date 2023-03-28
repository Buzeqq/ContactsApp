namespace ContactsApp.Dto.Contact;

public class GetContactsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }

    public static GetContactsDto Mapper(Models.Contact contact)
    {
        return new GetContactsDto { 
            Id = contact.Id, 
            Name = contact.Name, 
            Surname = contact.Surname, 
            Email = contact.Email 
        };
    }
}