using ContactsApp.Models;

namespace ContactsApp.Repository;

public interface ICategoryRepository
{
    IAsyncEnumerable<Category> GetCategoriesByNameAsync(string categoryName);
    IAsyncEnumerable<string> GetUniqueCategoryNames();

    IAsyncEnumerable<Category> GetAllCategories();

    Category? GetCategoryById(int categoryId);
    Category? GetCategoryByNameAndSubCategoryName(string name, string subCategoryName);
    Category CreateCategory(Category category);
    void DeleteCategory(Category category);
    void UpdateCategory(Category category);

    bool Exists(Category category);
}