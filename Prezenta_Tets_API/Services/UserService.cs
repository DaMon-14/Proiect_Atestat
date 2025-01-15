using Microsoft.EntityFrameworkCore;
using Prezenta_API.Models;
using Prezenta_API.EF;

namespace Prezenta_API.Services
{
    public class UserService : IUser
    {
        private readonly Context _db;
        public UserService(Context db)
        {
            _db = db;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _db.Users.Where(x=>x.UserId >0).ToListAsync();
        }

        public async Task<User> GetUserByUserId(uint id)
        {
            return await _db.Users.FirstOrDefaultAsync(user => user.UserId == id);
        }

        public async Task<User> AddUser(UpdateUser user) 
        {
            var adduser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
            };
            _db.Add(adduser);
            var result = await _db.SaveChangesAsync();
            return result >= 0 ? adduser : null;
        }

        public async Task<User> UpdateUser(uint id, UpdateUser userinfo)
        { 
            var User = await _db.Users.FirstOrDefaultAsync(index => index.UserId == id);
            if (User != null) 
            {
                User.FirstName = userinfo.FirstName;
                User.LastName = userinfo.LastName;
                User.Email = userinfo.Email;
                User.PhoneNumber = userinfo.PhoneNumber;
                var result = await _db.SaveChangesAsync();
                return result >= 0 ? User : null;
            }
                return null;
        }

        public async Task<bool> DeleteUser(uint id) 
        { 
            var User = await _db.Users.FirstOrDefaultAsync(index => index.UserId == id);
            if (User != null)
            {
                _db.Users.Remove(User);
                var result = await _db.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }
    }
}
