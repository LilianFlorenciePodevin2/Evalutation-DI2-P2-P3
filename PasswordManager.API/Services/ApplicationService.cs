// Services/ApplicationService.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.API.Domain;
using PasswordManager.API.Repositories;

namespace PasswordManager.API.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationService(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }

        public async Task<IEnumerable<Application>> GetAllApplicationsAsync()
        {
            return await _applicationRepository.GetAllAsync();
        }

        public async Task AddApplicationAsync(Application application)
        {
            await _applicationRepository.AddAsync(application);
        }
    }
}
