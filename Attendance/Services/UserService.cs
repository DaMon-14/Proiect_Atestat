using AttendanceAPI.Models;
using AttendanceAPI.EF;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace AttendanceAPI.Services
{
    public class UserService : IUser
    {
        private readonly AttendanceContext _db;
        public UserService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<List<UserDBO>> GetAllUsers()
        {
            return await _db.Users.Where(x => x.ClientId > 0).ToListAsync();
        }

        public async Task<UserDBO> GetUser(uint clientid)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.ClientId == clientid);
        }

        public async Task<UserDBO> AddUser(User client)
        {
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

        public async Task<UserDBO> UpdateUser(User clientinfo)
        {
            UserDBO client = await _db.Users.FirstOrDefaultAsync(x => x.ClientId == clientinfo.ClientId);
            if (client != null)
            {
                if(clientinfo.FirstName != null)
                {
                    client.FirstName = clientinfo.FirstName;
                }
                if(clientinfo.LastName != null)
                {
                    client.LastName = clientinfo.LastName;
                }
                if(clientinfo.Institution != null)
                {
                    client.Institution = clientinfo.Institution;
                }
                if(clientinfo.Email != null)
                {
                    client.Email = clientinfo.Email;
                }
                if(clientinfo.PhoneNumber != "")
                {
                    client.PhoneNumber = clientinfo.PhoneNumber;
                }
                if(clientinfo.Password != null)
                {
                    client.Password = clientinfo.Password;
                }
                if(clientinfo.UserName != null)
                {
                    client.UserName = clientinfo.UserName;
                }
                var results = await _db.SaveChangesAsync();
                return client;
            }
            return null;
        }

        public async Task<UserDBO> DeleteUser(uint clientid)
        {
            var client = await _db.Users.FirstOrDefaultAsync(x => x.ClientId == clientid);
            if (client != null)
            {
                _db.Users.Remove(client);
                await _db.SaveChangesAsync();
                return client;
            }
            return null;
        }

        public async Task<bool> AdminExists(User admin)
        {
            var client = await _db.Users.FirstOrDefaultAsync(x => x.UserName == admin.UserName && x.IsAdmin ==true);
            if (client != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CorectCredentials(User admin)
        {
            var client = await _db.Users.FirstOrDefaultAsync(x => x.UserName == admin.UserName && x.Password == admin.Password);
            if (client != null)
            {
                return true;
            }
            return false;
        }

        public async Task<uint> GetUserId(User admin)
        {
            var client = await _db.Users.FirstOrDefaultAsync(x => x.UserName == admin.UserName && x.Password == admin.Password);
            if (client != null)
            {
                return Convert.ToUInt16(client.ClientId);
            }
            return 0;
        }
    }
}
