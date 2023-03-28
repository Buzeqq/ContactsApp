using ContactsApp.Dto.Contact;
using ContactsApp.Models;
using ContactsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers;

[ApiController]
public class ContactsController
{
    private readonly IContactService _contactService;
    private readonly ICategoryService _categoryService;
    private readonly PasswordHasher<Contact> _passwordHasher;

    public ContactsController(IContactService contactService, ICategoryService categoryService)
    {
        _contactService = contactService;
        _categoryService = categoryService;
        _passwordHasher = new PasswordHasher<Contact>();
    }
    
    [HttpGet("/api/contacts")]
    public async IAsyncEnumerable<GetContactsDto> GetContacts()
    {
        await foreach (var contact in _contactService.GetContactsAsync())
        {
            yield return GetContactsDto.Mapper(contact);
        }
    }
    
    [Authorize]
    [HttpPost("/api/contacts")]
    public IResult CreateContact(PostContactsDto contact)
    {
        Contact newContact;
        
        try
        {
            var category = _categoryService.GetCategory(contact.Category, contact.Subcategory == "" ? null : contact.Subcategory);
            var mappedContact = PostContactsDto.Mapper(contact, category);
            mappedContact.Password = _passwordHasher.HashPassword(mappedContact, mappedContact.Password);
            newContact = _contactService.CreateContact(mappedContact);
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
    
    [Authorize]
    [HttpDelete("/api/contacts/{id:int}")]
    public IResult DeleteContact(int id)
    {
        _contactService.DeleteContact(id);
        return Results.Ok();
    }
}
