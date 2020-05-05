using BasicAPI.Entities;
using BasicAPI.Models;
using System;
using System.Security.Cryptography;

namespace BasicAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IPasswordService passwordService;

        public UserService(IPasswordService passwordService)
        {
            this.passwordService = passwordService;
        }

        public User CreateUser(UserModel login)
        {
            if (login == null)
            {
                throw new ArgumentNullException();
            }

            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string saltString = Convert.ToBase64String(salt);

            return new User
            {
                Salt = saltString,
                Password = passwordService.HashPassword(saltString, login.Password),
                Email = login.Email
            };
        }
    }
}