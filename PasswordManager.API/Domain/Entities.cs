namespace PasswordManager.API.Domain
{
    public class Password
    {
        public int Id { get; set; }
        // Mot de passe chiffré stocké en base
        public string EncryptedValue { get; set; }
        // Clé étrangère vers Application
        public int ApplicationId { get; set; }
        public Application Application { get; set; }
    }

    public class Application
    {
        public int Id { get; set; }
        public string AppName { get; set; }
        // "Grand public" ou "Professionnelle"
        public string AppType { get; set; }
    }
}
