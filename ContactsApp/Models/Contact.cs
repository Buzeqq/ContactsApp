namespace ContactsApp.Models;

public class Contact
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Category Category { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDay { get; set; }
}