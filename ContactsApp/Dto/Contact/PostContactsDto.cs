using System.Globalization;

namespace ContactsApp.Dto.Contact;

public class PostContactsDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    
    public string Category { get; set; }
    
    public string Subcategory { get; set; }
    public string BirthDay { get; set; }

    public static Models.Contact Mapper(PostContactsDto contact, Models.Category category)
    {
        return new Models.Contact {
            Name = contact.Name, 
            Surname = contact.Surname, 
            Email = contact.Email,
            PhoneNumber = contact.PhoneNumber,
            Password = contact.Password,
            BirthDay = DateTime.ParseExact(contact.BirthDay, "yyyy-MM-dd", new DateTimeFormatInfo()).ToUniversalTime(),
            Category = category
        };
    }
}