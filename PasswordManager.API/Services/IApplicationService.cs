// Services/IApplicationService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.API.Domain;

namespace PasswordManager.API.Services
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetAllApplicationsAsync();
        Task AddApplicationAsync(Application application);
    }
}
