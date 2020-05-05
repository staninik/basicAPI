using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;

namespace BasicAPI.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string salt, string password)
        {
            if (string.IsNullOrWhiteSpace(salt) || string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException();
            }

            byte[] saltBytes = Convert.FromBase64String(salt);

            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: saltBytes,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return hashedPassword;
        }

        public bool ArePasswordsEqual(string providedPassword, string providedPasswordSalt, string hashedPassword)
        {
            if (string.IsNullOrWhiteSpace(providedPassword) || 
                string.IsNullOrWhiteSpace(providedPasswordSalt) || 
                string.IsNullOrWhiteSpace(hashedPassword))
            {
                throw new ArgumentNullException();
            }

            return hashedPassword.Equals(HashPassword(providedPasswordSalt, providedPassword));
        }
    }
}