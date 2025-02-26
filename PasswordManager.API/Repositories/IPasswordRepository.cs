// Repositories/IPasswordRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.API.Domain;

namespace PasswordManager.API.Repositories
{
    public interface IPasswordRepository
    {
        Task<IEnumerable<Password>> GetAllAsync();
        Task<Password> GetByIdAsync(int id);
        Task AddAsync(Password password);
        Task DeleteAsync(int id);
    }
}
