namespace PasswordManager.API.DTOs
{
    // Pour la création d'un mot de passe (input)
    public class PasswordCreateDto
    {
        public string PlainPassword { get; set; }
        public int ApplicationId { get; set; }
        public string AppType { get; set; }
    }

    // Pour la réponse (output) avec le mot de passe décrypté
    public class PasswordResponseDto
    {
        public int Id { get; set; }
        public string PlainPassword { get; set; }
        public int ApplicationId { get; set; }
        public string AppName { get; set; }
        public string AppType { get; set; }
    }
}
