using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PasswordManager.API.Domain;
using PasswordManager.API.Services;

namespace PasswordManager.API.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            // Si des applications existent déjà, la DB est considérée comme initialisée.
            if (context.Applications.Any())
            {
                return;
            }

            // Ajout de quelques applications
            var appPublic = new Application { AppName = "Application Public", AppType = "Grand public" };
            var appPro = new Application { AppName = "Application Pro", AppType = "Professionnelle" };

            context.Applications.AddRange(appPublic, appPro);
            context.SaveChanges();

            // Obtenir le service d'encryptage depuis le conteneur DI
            var encryptionService = serviceProvider.GetRequiredService<EncryptionService>();

            // Génération des mots de passe encryptés en utilisant le service
            // Pour RSA, le service doit utiliser la même paire de clés persistante (ex: via DataProtection)
            var encryptedPublic = encryptionService.Encrypt("Public123", "Grand public");
            var encryptedPro = encryptionService.Encrypt("Pro456", "Professionnelle");

            var password1 = new Password { EncryptedValue = encryptedPublic, ApplicationId = appPublic.Id };
            var password2 = new Password { EncryptedValue = encryptedPro, ApplicationId = appPro.Id };

            context.Passwords.AddRange(password1, password2);
            context.SaveChanges();
        }
    }
}
