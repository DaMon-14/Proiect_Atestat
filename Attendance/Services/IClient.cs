using Attendance.Models;

namespace Attendance.Services
{
    public interface IClient
    {
        Task<List<Client>> GetAllClients();
        Task<Client> GetClient(uint clientid);
        Task<Client> AddClient(UpdateClient client);
        Task<Client> UpdateClient(uint clientid, UpdateClient clientinfo);
    }
}
