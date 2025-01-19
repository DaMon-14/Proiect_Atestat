using AttendanceAPI.Models;

namespace AttendanceAPI.Services
{
    public interface IClient
    {
        Task<List<Client>> GetAllClients();
        Task<Client> GetClient(uint clientid);
        Task<Client> AddClient(UpdateClient client);
        Task<Client> UpdateClient(Client clientinfo);
        Task<Client> DeleteClient(uint clientid);
    }
}
