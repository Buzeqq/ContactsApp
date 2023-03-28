using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsApp.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase
{
    [HttpGet("/api/user")]
    public Dictionary<string, string> GetUser()
    {
        return User.Claims.ToDictionary(x => x.Type, x => x.Value);
    }

    [HttpPost("/api/login")]
    public async Task<IResult> Login(LoginForm form, SignInManager<IdentityUser> signInManager)
    {
        var result =  await signInManager.
            PasswordSignInAsync(form.Username, form.Password, true, false);

        return result.Succeeded ? Results.Ok() : Results.BadRequest();
    }

    [Authorize]
    [HttpGet("/api/logout")]
    public async Task<IResult> SignOut(SignInManager<IdentityUser> signInManager)
    {
        await signInManager.SignOutAsync();
        return Results.Ok();
    }

    [HttpPost("/api/register")]
    public async Task<IResult> Register(RegisterForm form, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        if (form.Password != form.ConfirmPassword)
        {
            return Results.BadRequest();
        }
        
        Console.WriteLine(form.Username);
        Console.WriteLine(form.Password);
        var user = new IdentityUser { UserName = form.Username };
        var createUserResult = await userManager.CreateAsync(user, form.Password);
        if (!createUserResult.Succeeded)
        {
            return Results.BadRequest(); 
        }
        
        await signInManager.SignInAsync(user, true);

        return Results.Ok();
    }
}

public class LoginForm
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegisterForm
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}