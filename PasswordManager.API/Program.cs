using Microsoft.EntityFrameworkCore;
using PasswordManager.API.Data;
using PasswordManager.API.Repositories.Interfaces;
using PasswordManager.API.Repositories;
using PasswordManager.API.Services.Encryption;
using PasswordManager.API.Services.Interfaces;
using PasswordManager.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuration du DbContext avec la cha�ne de connexion
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Ajout des services pour les controllers et Swagger si besoin
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injection de d�pendances pour vos services et repositories
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
// Ajoutez les services de chiffrement ici �galement, par exemple pour AES et RSA
builder.Services.AddScoped<IAEService, AESEncryptionService>();
builder.Services.AddScoped<IRSAService, RSAEncryptionService>();

var app = builder.Build();

// Configuration du middleware Swagger en d�veloppement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
