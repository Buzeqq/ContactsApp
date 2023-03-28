using ContactsApp.Repository;
using ContactsApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DbContext = ContactsApp.Database.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbContext>(c => c.UseNpgsql(
    builder.Configuration.GetConnectionString("Default")
    ));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<DbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IContactService, ContactService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(_ => { });

app.Use((ctx, next) =>
{
    if (!ctx.Request.Path.StartsWithSegments("/api")) return next();

    ctx.Response.StatusCode = 404;
    return Task.CompletedTask;
});

app.UseSpa(x => { x.UseProxyToSpaDevelopmentServer("http://127.0.0.1:4200"); });

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
