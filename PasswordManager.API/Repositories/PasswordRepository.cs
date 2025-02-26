// Repositories/PasswordRepository.cs
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PasswordManager.API.Data;
using PasswordManager.API.Domain;

namespace PasswordManager.API.Repositories
{
    public class PasswordRepository : IPasswordRepository
    {
        private readonly ApplicationDbContext _context;

        public PasswordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Password>> GetAllAsync()
        {
            return await _context.Passwords.Include(p => p.Application).ToListAsync();
        }

        public async Task<Password> GetByIdAsync(int id)
        {
            return await _context.Passwords.FindAsync(id);
        }

        public async Task AddAsync(Password password)
        {
            _context.Passwords.Add(password);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var password = await _context.Passwords.FindAsync(id);
            if (password != null)
            {
                _context.Passwords.Remove(password);
                await _context.SaveChangesAsync();
            }
        }
    }
}
