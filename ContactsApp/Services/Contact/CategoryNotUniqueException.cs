using ContactsApp.Models;

namespace ContactsApp.Services;

public class CategoryNotUniqueException: Exception
{
    public CategoryNotUniqueException(Category category): 
        base($"Tried to create category that already exists: {category.Name}:{category.SubCategoryName}") { }
}