namespace ContactsApp.Dto.Contact;

public class GetContactDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Category { get; set; }
    public string SubCategory { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDay { get; set; }

    public static GetContactDto Mapper(Models.Contact contact)
    {
        return new GetContactDto
        {
            Id = contact.Id,
            Name = contact.Name,
            Surname = contact.Surname,
            Email = contact.Email,
            Category = contact.Category.Name,
            SubCategory = contact.Category.SubCategoryName,
            PhoneNumber = contact.PhoneNumber,
            BirthDay = contact.BirthDay
        };
    }
}