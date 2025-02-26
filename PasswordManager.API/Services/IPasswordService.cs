using System.Collections.Generic;
using System.Threading.Tasks;
using PasswordManager.API.DTOs;

namespace PasswordManager.API.Services
{
    public interface IPasswordService
    {
        Task<IEnumerable<PasswordResponseDto>> GetAllPasswordsAsync();
        Task AddPasswordAsync(string accountName, string plainPassword, int applicationId);
        Task DeletePasswordAsync(int id);
    }
}
