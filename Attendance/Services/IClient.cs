using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Services
{
    public interface IClient
    {
        Task<List<ClientDBO>> GetAllClients();
        Task<ClientDBO> GetClient(uint clientid);
        Task<ClientDBO> AddClient(Client client);
        Task<ClientDBO> UpdateClient(ClientDBO clientinfo);
        Task<ClientDBO> DeleteClient(uint clientid);
    }
}
