using Microsoft.EntityFrameworkCore;
using PasswordManager.API.Data;
using PasswordManager.API.Domain;
using PasswordManager.API.Services;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var context = new ApplicationDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

        if (context.Applications.Any())
        {
            return;
        }

        var appPublic = new Application { AppName = "Application Public", AppType = "Grand public" };
        var appPro = new Application { AppName = "Application Pro", AppType = "Professionnelle" };

        context.Applications.AddRange(appPublic, appPro);
        context.SaveChanges();

        var encryptionService = serviceProvider.GetRequiredService<EncryptionService>();

        var encryptedPublic = encryptionService.Encrypt("Public123", "Grand public");
        var encryptedPro = encryptionService.Encrypt("Pro456", "Professionnelle");

        var password1 = new Password
        {
            AccountName = "ComptePublic",
            EncryptedValue = encryptedPublic,
            ApplicationId = appPublic.Id
        };
        var password2 = new Password
        {
            AccountName = "ComptePro",
            EncryptedValue = encryptedPro,
            ApplicationId = appPro.Id
        };

        context.Passwords.AddRange(password1, password2);
        context.SaveChanges();
    }
}
