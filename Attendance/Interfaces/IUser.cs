using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IUser
    {
        Task<List<UserDBO>> GetAllClients(string UID);
        Task<UserDBO> GetClient(uint clientid, string UID);
        Task<UserDBO> AddClient(User client, string UID);
        Task<UserDBO> UpdateClient(User clientinfo,string UID);
        Task<UserDBO> DeleteClient(uint clientid, string UID);
        Task<bool> AdminExists(User admin, string UID);
        Task<bool> CorectCredentials(User admin, string UID);
        Task<uint> GetUserId(User admin, string UID);
    }
}
