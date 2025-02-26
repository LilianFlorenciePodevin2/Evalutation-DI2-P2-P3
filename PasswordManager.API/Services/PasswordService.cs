// Services/PasswordService.cs
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PasswordManager.API.Domain;
using PasswordManager.API.DTOs;
using PasswordManager.API.Repositories;

namespace PasswordManager.API.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly IPasswordRepository _passwordRepository;
        private readonly IApplicationRepository _applicationRepository;
        private readonly EncryptionService _encryptionService;

        public PasswordService(IPasswordRepository passwordRepository,
                               IApplicationRepository applicationRepository,
                               EncryptionService encryptionService)
        {
            _passwordRepository = passwordRepository;
            _applicationRepository = applicationRepository;
            _encryptionService = encryptionService;
        }

        public async Task<IEnumerable<PasswordResponseDto>> GetAllPasswordsAsync()
        {
            var passwords = await _passwordRepository.GetAllAsync();
            var dtos = passwords.Select(p => new PasswordResponseDto
            {
                Id = p.Id,
                AccountName = p.AccountName,
                ApplicationId = p.ApplicationId,
                AppName = p.Application.AppName,
                AppType = p.Application.AppType,
                PlainPassword = _encryptionService.Decrypt(p.EncryptedValue, p.Application.AppType)
            });
            return dtos;
        }

        public async Task AddPasswordAsync(string accountName, string plainPassword, int applicationId)
        {
            // Récupérer l'application par son ID
            var application = await _applicationRepository.GetByIdAsync(applicationId);
            if (application == null)
            {
                throw new System.Exception("Application non trouvée");
            }

            // Utiliser le type d'application récupéré pour le chiffrement
            var encryptedPassword = _encryptionService.Encrypt(plainPassword, application.AppType);
            var password = new Password
            {
                AccountName = accountName,
                EncryptedValue = encryptedPassword,
                ApplicationId = applicationId
            };
            await _passwordRepository.AddAsync(password);
        }

        public async Task DeletePasswordAsync(int id)
        {
            await _passwordRepository.DeleteAsync(id);
        }
    }
}
