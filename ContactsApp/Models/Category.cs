namespace ContactsApp.Models;

public class Category
{
    public int Id { get; set; }
    public string Name { set; get; }
    public string? SubCategoryName { get; set; }
    
    public List<Contact> Contacts { get; set; }
}