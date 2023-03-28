using ContactsApp.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = ContactsApp.Database.DbContext;

namespace ContactsApp.Repository;

public class CategoryRepository: ICategoryRepository, IDisposable
{
    private readonly DbContext _dbContext;

    public CategoryRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IAsyncEnumerable<Category> GetCategoriesByNameAsync(string categoryName)
    {
        return _dbContext.Categories.Where(category => category.Name == categoryName).AsAsyncEnumerable();
    }

    public IAsyncEnumerable<string> GetUniqueCategoryNames()
    {
        return _dbContext.Categories.Select(category => category.Name).Distinct().AsAsyncEnumerable();
    }

    public IAsyncEnumerable<Category> GetAllCategories()
    {
        return _dbContext.Categories.AsAsyncEnumerable();
    }

    public Category? GetCategoryById(int categoryId)
    {
        return _dbContext.Categories.Find(categoryId);
    }

    public Category? GetCategoryByNameAndSubCategoryName(string name, string subCategoryName)
    {
        return _dbContext.Categories
            .FirstOrDefault(category => category.Name == name && category.SubCategoryName == subCategoryName);
    }

    public void CreateCategory(Category newCategory)
    {
        _dbContext.Categories.Add(newCategory);
        _dbContext.SaveChanges();
    }

    public void DeleteCategory(Category category)
    {
        _dbContext.Categories.Remove(category);
        _dbContext.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        _dbContext.Categories.Update(category);
        _dbContext.SaveChanges();
    }

    public bool Exists(Category newCategory)
    {
        return _dbContext.Categories.Any(category =>
            category.Name == newCategory.Name && category.SubCategoryName == newCategory.SubCategoryName);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}