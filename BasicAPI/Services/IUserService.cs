using BasicAPI.Entities;
using BasicAPI.Models;

namespace BasicAPI.Services
{
    public interface IUserService
    {
        User CreateUser(UserModel model);
    }
}