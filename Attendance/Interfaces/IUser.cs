using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IUser
    {
        Task<List<UserInfo>> GetAllUsers();
        Task<UserDBO> GetUser(uint clientid);
        Task<UserDBO> AddUser(UpdateUser client);
        Task<UserDBO> UpdateUser(UpdateUser clientinfo);
        Task<UserDBO> DeleteUser(uint clientid);
        Task<bool> AdminExists(UpdateUser admin);
        Task<bool> CorectCredentials(UpdateUser admin);
        Task<uint> GetUserId(UpdateUser admin);
    }
}
