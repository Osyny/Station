namespace Station.Web.Services.PasswordHashers
{
    public class PasswordHasher : IPasswordHasher
    {
        public PasswordHasher() { }
        public string GetHashadPasword(string password)
        {
            var hashadPasword = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return hashadPasword;
        }
        public bool Veryfy(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
        }
    }
}
