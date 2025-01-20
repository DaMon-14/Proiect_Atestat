using AttendanceAPI.EF;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using AttendanceAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AttendanceAPI.Services
{
    public class AdminService : IAdmin
    {
        private readonly AttendanceContext _db;
        private readonly IConfiguration _configuration;
        public AdminService(AttendanceContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public async Task<AdminDBO> AddAdmin(Admin addAdmin, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            AdminDBO admin = new AdminDBO
            {
                Password = addAdmin.Password,
            };
            _db.Admins.Add(admin);
            await _db.SaveChangesAsync();
            return admin;
        }

        public async Task<AdminDBO> UpdateAdmin(AdminDBO updateadmin, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }

            var admin = _db.Admins.FirstOrDefault(x => x.Id == updateadmin.Id);
            admin.Password = updateadmin.Password;

            var results = await _db.SaveChangesAsync();
            return admin;
        }

        public async Task<bool> AdminExists(GetAdmin admin, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return false;
            }
            AdminDBO getadmin = await _db.Admins.FirstOrDefaultAsync(x => x.Id == admin.Id && x.Password == admin.Password);
            if (getadmin == null)
            {
                return false;
            }
            return true;
        }
    }
}
