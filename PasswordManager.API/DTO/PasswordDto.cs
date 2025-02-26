// DTOs/PasswordDto.cs
namespace PasswordManager.API.DTOs
{
    public class PasswordCreateDto
    {
        public string AccountName { get; set; } // Nom du compte
        public string PlainPassword { get; set; }
        public int ApplicationId { get; set; }
    }

    public class PasswordResponseDto
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string PlainPassword { get; set; }
        public int ApplicationId { get; set; }
        public string AppName { get; set; }
        public string AppType { get; set; }
    }
}
