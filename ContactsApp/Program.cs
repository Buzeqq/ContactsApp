using ContactsApp.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<Database>(c => c.UseNpgsql(
    builder.Configuration.GetConnectionString("Default")
    ));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<Database>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthorization();

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