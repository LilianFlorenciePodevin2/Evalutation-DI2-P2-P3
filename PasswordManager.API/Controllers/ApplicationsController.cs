using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PasswordManager.API.DTOs;
using PasswordManager.API.Domain;
using PasswordManager.API.Services;

namespace PasswordManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetApplications()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            // Mapping des entités vers DTO
            var dtos = applications.Select(a => new ApplicationDto
            {
                Id = a.Id,
                AppName = a.AppName,
                AppType = a.AppType
            });
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<IActionResult> AddApplication([FromBody] ApplicationDto dto)
        {
            var application = new Application
            {
                AppName = dto.AppName,
                AppType = dto.AppType
            };
            await _applicationService.AddApplicationAsync(application);
            return Ok("Application ajoutée");
        }
    }
}
