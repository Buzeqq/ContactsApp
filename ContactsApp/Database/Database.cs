using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ContactsApp.Database;

public class Database : IdentityDbContext<IdentityUser>
{
    public Database(DbContextOptions<Database> options) : base(options) { }
}