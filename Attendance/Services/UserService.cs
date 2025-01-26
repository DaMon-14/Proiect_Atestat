using AttendanceAPI.Models;
using AttendanceAPI.EF;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AttendanceAPI.Services
{
    public class UserService : IUser
    {
        private readonly AttendanceContext _db;
        private readonly IConfiguration _configuration;
        public UserService(AttendanceContext db, IConfiguration config)
        {
            _db = db;
            _configuration = config;
        }

        public async Task<List<UserDBO>> GetAllClients(string UID)
        {
            if(UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            return await _db.Users.Where(x => x.ClientId > 0).ToListAsync();
        }

        public async Task<UserDBO> GetClient(uint clientid, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            return await _db.Users.FirstOrDefaultAsync(x => x.ClientId == clientid);
        }

        public async Task<UserDBO> AddClient(User client, string UID)
        {
            if(UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            var newClient = new UserDBO
            {
                FirstName = client.FirstName,
                LastName = client.LastName,
                Institution = client.Institution,
                Email = client.Email,
                PhoneNumber = client.PhoneNumber,
                UserName = client.UserName,
                Password = client.Password,
                IsAdmin = false,
                Salt = DateTime.UtcNow.ToString()
            };
            _db.Users.Add(newClient);
            await _db.SaveChangesAsync();
            return newClient;
        }

        public async Task<UserDBO> UpdateClient(User clientinfo, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            var client = await _db.Users.FirstOrDefaultAsync(x => x.ClientId == clientinfo.ClientId);
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

        public async Task<UserDBO> DeleteClient(uint clientid, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            var client = await _db.Users.FirstOrDefaultAsync(x => x.ClientId == clientid);
            if (client != null)
            {
                _db.Users.Remove(client);
                await _db.SaveChangesAsync();
                return client;
            }
            return null;
        }

        public async Task<bool> AdminExists(User admin, string UID)
        {
            if(UID != _configuration.GetValue<string>("UID"))
            {
                return false;
            }
            var client = await _db.Users.FirstOrDefaultAsync(x => x.UserName == admin.UserName && x.IsAdmin ==true);
            if (client != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CorectCredentials(User admin, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return false;
            }
            var client = await _db.Users.FirstOrDefaultAsync(x => x.UserName == admin.UserName && x.Password == admin.Password);
            if (client != null)
            {
                return true;
            }
            return false;
        }
    }
}
