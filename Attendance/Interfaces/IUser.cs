using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IUser
    {
        Task<List<UserInfo>> GetAllUsers();
        Task<UserInfo> GetUser(uint clientid);
        Task<UserDBO> AddUser(UpdateUser client);
        Task<UserDBO> UpdateUser(UpdateUser clientinfo);
        Task<UserDBO> DeleteUser(uint clientid);
        Task<bool> CorectCredentials(UpdateUser admin);
        Task<UserInfo> GetUserByUsername(UpdateUser admin);
    }
}
