namespace Station.Web.Services.PasswordHashers
{
    public interface IPasswordHasher
    {
        string GetHashadPasword(string password);
        bool Veryfy(string password, string passwordHash);
    }
}
