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
            // Récupère les mots de passe avec leur application (inclusion via EF Core)
            var passwords = await _passwordRepository.GetAllAsync();

            // Mappe les entités vers des DTOs en décryptant le mot de passe
            var dtos = passwords.Select(p => new PasswordResponseDto
            {
                Id = p.Id,
                ApplicationId = p.ApplicationId,
                AppName = p.Application.AppName,
                AppType = p.Application.AppType,
                PlainPassword = _encryptionService.Decrypt(p.EncryptedValue, p.Application.AppType)
            });

            return dtos;
        }

        public async Task AddPasswordAsync(string plainPassword, int applicationId, string appType)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId);
            if (application == null)
            {
                throw new Exception("L'application spécifiée n'existe pas.");
            }

            var encryptedPassword = _encryptionService.Encrypt(plainPassword, appType);
            var password = new Password
            {
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
