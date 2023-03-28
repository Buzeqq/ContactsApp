using ContactsApp.Dto.Category;
using ContactsApp.Models;
using ContactsApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers;

[ApiController]
public class CategoryController
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }
    
    [HttpGet("/api/categories")]
    public IAsyncEnumerable<string> GetCategoriesNames()
    {
        return _service.GetUniqueCategoryNames();
    }

    [HttpGet("/api/categories/{name}")]
    public async IAsyncEnumerable<string?> GetSubcategoriesByCategoryName(string name)
    {
        await foreach (var category in _service.GetCategoriesByNameAsync(name))
        {
            yield return category.SubCategoryName;
        }
    }
    
    [HttpGet("/api/categories/{id:int}")]
    public IResult GetCategory(int id)
    {
        Category category;
        try
        {
            category = _service.GetCategory(id);
            return Results.Ok(GetCategoryDto.Mapper(category));
        }
        catch (Exception)
        {
            return Results.NotFound();
        }
    }

    [HttpPost("/api/categories/{name}")]
    public IResult CreateCategory(string name, [FromBody] CreateCategoryDto subcategory)
    {
        if (name != "other")
        {
            return Results.NotFound();
        }
        
        var category = _service.CreateCategory(CreateCategoryDto.Mapper(subcategory));
        return Results.Created("/api/categories/", category.Id);
    }
}


