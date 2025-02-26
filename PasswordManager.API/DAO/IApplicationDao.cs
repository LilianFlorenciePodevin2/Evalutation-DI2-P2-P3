// DAO/IApplicationDao.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.API.Domain;

namespace PasswordManager.API.DAO
{
    public interface IApplicationDao
    {
        Task<IEnumerable<Application>> GetAllAsync();
        Task<Application> GetByIdAsync(int id);
        Task AddAsync(Application application);
    }
}
