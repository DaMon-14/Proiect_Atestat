using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IUser
    {
        Task<List<UserDBO>> GetAllUsers();
        Task<UserDBO> GetUser(uint clientid);
        Task<UserDBO> AddUser(User client);
        Task<UserDBO> UpdateUser(User clientinfo);
        Task<UserDBO> DeleteUser(uint clientid);
        Task<bool> AdminExists(User admin);
        Task<bool> CorectCredentials(User admin);
        Task<uint> GetUserId(User admin);
    }
}
