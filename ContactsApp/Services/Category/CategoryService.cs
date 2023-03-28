using ContactsApp.Models;
using ContactsApp.Repository;

namespace ContactsApp.Services;

public class CategoryService: ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public IAsyncEnumerable<Category> GetCategoriesByNameAsync(string category)
    {
        return _repository.GetCategoriesByNameAsync(category);
    }

    public IAsyncEnumerable<string> GetUniqueCategoryNames()
    {
        return _repository.GetUniqueCategoryNames();
    }

    public IAsyncEnumerable<Category> GetAllCategories()
    {
        return _repository.GetAllCategories();
    }

    public Category GetCategory(string name, string? subCategoryName)
    {
        var category = _repository.GetCategoryByNameAndSubCategoryName(name, subCategoryName);
        if (category == null) throw new Exception();

        return category;
    }

    public Category GetCategory(int id)
    {
        var category = _repository.GetCategoryById(id);
        if (category == null) throw new Exception(); // FIXME: add custom exception

        return category;
    }

    public Category CreateCategory(Category category)
    {
        if (_repository.Exists(category))
        {
            throw new CategoryNotUniqueException(category);
        }
        
        return _repository.CreateCategory(category);
    }

    public void DeleteCategory(int id)
    {
        var category = _repository.GetCategoryById(id);
        if (category == null) return; // FIXME throw
        _repository.DeleteCategory(category);
    }

    public void UpdateCategory(Category category)
    {
        _repository.UpdateCategory(category);
    }
}