// Controllers/PasswordsController.cs
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
            // Utiliser uniquement AccountName, PlainPassword et ApplicationId
            await _passwordService.AddPasswordAsync(dto.AccountName, dto.PlainPassword, dto.ApplicationId);
            return Ok(new { message = "Mot de passe ajouté" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePassword(int id)
        {
            await _passwordService.DeletePasswordAsync(id);
            return Ok(new { message = "Mot de passe supprimé" });
        }
    }
}
