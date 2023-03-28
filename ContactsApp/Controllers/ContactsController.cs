using ContactsApp.Dto.Contact;
using ContactsApp.Models;
using ContactsApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers;

[ApiController]
public class ContactsController
{
    private readonly IContactService _contactService;
    private readonly ICategoryService _categoryService;

    public ContactsController(IContactService contactService, ICategoryService categoryService)
    {
        _contactService = contactService;
        _categoryService = categoryService;
    }
    
    [HttpGet("/api/contacts")]
    public async IAsyncEnumerable<GetContactsDto> GetContacts()
    {
        await foreach (var contact in _contactService.GetContactsAsync())
        {
            yield return GetContactsDto.Mapper(contact);
        }
    }

    [HttpPost("/api/contacts")]
    public IResult CreateContact(PostContactsDto contact)
    {
        Contact newContact;
        try
        {
            var category = _categoryService.GetCategory(contact.Category, contact.Subcategory);
            newContact = _contactService.CreateContact(PostContactsDto.Mapper(contact, category));
        }
        catch
        {
            return Results.BadRequest();
        }
        
        return Results.Created("/api/contacts", newContact.Id);
    }
    
    [HttpGet("/api/contacts/{id:int}")]
    public IResult GetContact(int id)
    {
        Contact contact;
        try
        {
            contact = _contactService.GetContactById(id);
        }
        catch (ContactNotFoundException)
        {
            return Results.NotFound();
        }
        
        return Results.Ok(GetContactDto.Mapper(contact));
    }
}
