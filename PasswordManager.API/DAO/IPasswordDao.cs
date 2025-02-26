using PasswordManager.API.Domain;

namespace PasswordManager.API.DAO
{
    public interface IPasswordDao
    {
        Task<IEnumerable<Password>> GetAllAsync();
        Task<Password> GetByIdAsync(int id);
        Task AddAsync(Password password);
        Task DeleteAsync(int id);
    }
}
