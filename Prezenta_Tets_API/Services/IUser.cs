using Prezenta_API.Models;

namespace Prezenta_API.Services
{
    public interface IUser
    {
        Task<List<User>> GetAllUsers();
        Task<User> GetUserByUserId(uint id);
        Task<User> AddUser(UpdateUser user);
        Task<User> UpdateUser(uint id, UpdateUser userinfo);
        Task<bool> DeleteUser(uint id);
    }
}
