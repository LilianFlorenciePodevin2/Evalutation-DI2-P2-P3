using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.DTOs;
using PasswordManager.API.Services;

namespace PasswordManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PasswordsController : ControllerBase
    {
        private readonly IPasswordService _passwordService;

        public PasswordsController(IPasswordService passwordService)
        {
            _passwordService = passwordService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPasswords()
        {
            var passwords = await _passwordService.GetAllPasswordsAsync();
            return Ok(passwords);
        }

        [HttpPost]
        public async Task<IActionResult> AddPassword([FromBody] PasswordCreateDto dto)
        {
            // Le DTO contient le mot de passe en clair, l'ID de l'application et le type d'application
            await _passwordService.AddPasswordAsync(dto.PlainPassword, dto.ApplicationId, dto.AppType);
            return Ok("Mot de passe ajouté");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassword(int id)
        {
            await _passwordService.DeletePasswordAsync(id);
            return Ok("Mot de passe supprimé");
        }
    }
}
