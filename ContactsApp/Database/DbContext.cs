using ContactsApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Database;

public class DbContext : IdentityDbContext<IdentityUser>
{
    public DbContext(DbContextOptions<DbContext> options) : base(options) { }

    public DbSet<Contact> Contacts { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Contact>()
            .HasIndex(contact => contact.Email)
            .IsUnique();

        builder.Entity<Contact>()
            .HasOne(contact => contact.Category)
            .WithMany(category => category.Contacts);

        builder.Entity<Category>()
            .HasData(
                new Category { Id = 1, Name = "private" }, 
                new Category { Id = 2, Name = "other" }, 
                new Category { Id = 3, Name = "business", SubCategoryName = "boss" }, 
                new Category { Id = 4, Name = "business", SubCategoryName = "manager" }, 
                new Category { Id = 5, Name = "business", SubCategoryName = "intern" }, 
                new Category { Id = 6, Name = "business", SubCategoryName = "team leader" }, 
                new Category { Id = 7, Name = "business", SubCategoryName = "hr" }, 
                new Category { Id = 8, Name = "business", SubCategoryName = "secretary" });

        base.OnModelCreating(builder);
    }
}