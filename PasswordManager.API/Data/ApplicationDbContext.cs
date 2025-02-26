using Microsoft.EntityFrameworkCore;
using PasswordManager.API.Models; // Assurez-vous de référencer vos entités

namespace PasswordManager.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Password> Passwords { get; set; }
        public DbSet<Application> Applications { get; set; }

        // Vous pouvez ajouter la configuration des entités dans OnModelCreating si nécessaire
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configuration Fluent API ici
        }
    }
}
