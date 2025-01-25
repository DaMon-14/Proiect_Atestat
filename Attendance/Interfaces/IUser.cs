using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IUser
    {
        Task<List<UserDBO>> GetAllClients();
        Task<UserDBO> GetClient(uint clientid);
        Task<UserDBO> AddClient(User client);
        Task<UserDBO> UpdateClient(UserDBO clientinfo);
        Task<UserDBO> DeleteClient(uint clientid);
        Task<bool> AdminExists(User admin, string UID);
        Task<bool> CorectCredentials(User admin, string UID);
    }
}
