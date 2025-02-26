using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PasswordManager.API.Data;
using PasswordManager.API.Middlewares;
using PasswordManager.API.Repositories;
using PasswordManager.API.Services;
using PasswordManager.API.Encryption;
using Microsoft.AspNetCore.DataProtection;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Ajout de Data Protection
builder.Services.AddDataProtection();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPasswordRepository, PasswordRepository>();
builder.Services.AddScoped<IApplicationRepository, ApplicationRepository>();

// Enregistrer AES en Scoped et RSA en Singleton
builder.Services.AddScoped<AesEncryptionStrategy>();
builder.Services.AddSingleton<RsaEncryptionStrategy>();

builder.Services.AddScoped<EncryptionService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder
            .WithOrigins("http://localhost:4200") // Angular app URL
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Configuration de Swagger avec sécurité API Key
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PasswordManager.API", Version = "v1" });

    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "Entrez votre clé API. Exemple: SuperSecretAPIKey2025",
        In = ParameterLocation.Header,
        Name = "x-api-key",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

var app = builder.Build();

// Seed de la base de données
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // Exécuter les migrations si nécessaire
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();

        // Initialiser les données de seed
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        // Vous pouvez loguer l'erreur ici
        throw;
    }
}

// Activation de Swagger en mode développement
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PasswordManager.API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseCors("AllowSpecificOrigin");

// Middleware de clé API
app.UseMiddleware<ApiKeyMiddleware>();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
