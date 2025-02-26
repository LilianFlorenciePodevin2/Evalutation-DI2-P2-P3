using Microsoft.EntityFrameworkCore;
using PasswordManager.API.Domain;

namespace PasswordManager.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        public DbSet<Password> Passwords { get; set; }
        public DbSet<Application> Applications { get; set; }
    }
}
