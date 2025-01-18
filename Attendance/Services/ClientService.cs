using Attendance.Models;
using Attendance.EF;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Services
{
    public class ClientService : IClient
    {
        private readonly AttendanceContext _db;
        public ClientService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<List<Client>> GetAllClients()
        {
            return await _db.Clients.Where(x => x.ClientId > 0).ToListAsync();
        }

        public async Task<Client> GetClient(uint clientid)
        {
            return await _db.Clients.FirstOrDefaultAsync(x => x.ClientId == clientid);
        }

        public async Task<Client> AddClient(UpdateClient client)
        {
            var newClient = new Client
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

        public async Task<Client> UpdateClient(Client clientinfo)
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

        public async Task<Client> DeleteClient(uint clientid)
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
