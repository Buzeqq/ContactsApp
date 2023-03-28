using ContactsApp.Models;

namespace ContactsApp.Services;

public interface ICategoryService
{
       IAsyncEnumerable<Category> GetCategoriesByNameAsync(string category);

       IAsyncEnumerable<string> GetUniqueCategoryNames();

       IAsyncEnumerable<Category> GetAllCategories();

       Category GetCategory(string name, string? subCategoryName);

       Category GetCategory(int id);
       Category CreateCategory(Category category);
       void DeleteCategory(int id);
       void UpdateCategory(Category category);
       
}