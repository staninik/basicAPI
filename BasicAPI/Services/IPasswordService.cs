namespace BasicAPI.Services
{
    public interface IPasswordService
    {
        string HashPassword(string salt, string password);
        
        bool ArePasswordsEqual(string providedPassword, string providedPasswordSalt, string hashedPassword);
    }
}