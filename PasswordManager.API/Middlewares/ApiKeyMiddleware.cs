using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace PasswordManager.API.Middlewares
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;
        private const string APIKEY_HEADER = "x-api-key";
        private readonly string _apiKey;

        public ApiKeyMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            // Récupération de la clé API depuis la section "ApiSettings"
            _apiKey = configuration.GetValue<string>("ApiSettings:ApiKey");
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!context.Request.Headers.TryGetValue(APIKEY_HEADER, out var extractedApiKey))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("API Key manquante");
                return;
            }

            if (!extractedApiKey.Equals(_apiKey))
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync("Clé API invalide");
                return;
            }

            await _next(context);
        }
    }
}
