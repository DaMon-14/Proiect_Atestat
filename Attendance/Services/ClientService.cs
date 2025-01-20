using AttendanceAPI.Models;
using AttendanceAPI.EF;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;

namespace AttendanceAPI.Services
{
    public class ClientService : IClient
    {
        private readonly AttendanceContext _db;
        public ClientService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<List<ClientDBO>> GetAllClients()
        {
            return await _db.Clients.Where(x => x.ClientId > 0).ToListAsync();
        }

        public async Task<ClientDBO> GetClient(uint clientid)
        {
            return await _db.Clients.FirstOrDefaultAsync(x => x.ClientId == clientid);
        }

        public async Task<ClientDBO> AddClient(Client client)
        {
            var newClient = new ClientDBO
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Institution = client.Institution,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber
            };
            _db.Clients.Add(newClient);
            await _db.SaveChangesAsync();
            return newClient;
        }

        public async Task<ClientDBO> UpdateClient(ClientDBO clientinfo)
        {
            var client = await _db.Clients.FirstOrDefaultAsync(x => x.ClientId == clientinfo.ClientId);
            if (client != null)
            {
                client.FirstName = clientinfo.FirstName;
                client.LastName = clientinfo.LastName;
                client.Institution = clientinfo.Institution;
                client.Email = clientinfo.Email;
                client.PhoneNumber = clientinfo.PhoneNumber;
                var results = await _db.SaveChangesAsync();
                return client;
            }
            return null;
        }

        public async Task<ClientDBO> DeleteClient(uint clientid)
        {
            var client = await _db.Clients.FirstOrDefaultAsync(x => x.ClientId == clientid);
            if (client != null)
            {
                _db.Clients.Remove(client);
                await _db.SaveChangesAsync();
                return client;
            }
            return null;
        }
    }
}
