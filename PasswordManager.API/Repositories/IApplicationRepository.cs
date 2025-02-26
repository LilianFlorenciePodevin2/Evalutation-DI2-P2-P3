// Repositories/IApplicationRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.API.Domain;

namespace PasswordManager.API.Repositories
{
    public interface IApplicationRepository
    {
        Task<IEnumerable<Application>> GetAllAsync();
        Task<Application> GetByIdAsync(int id);
        Task AddAsync(Application application);
    }
}
