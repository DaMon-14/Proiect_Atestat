using AttendanceAPI.Models;

namespace AttendanceAPI.Services
{
    public interface IAdmin
    {
        public Task<Admin> AddAdmin(UpdateAdmin addAdmin, string UID);
        public Task<Admin> UpdateAdmin(Admin admin, string UID);
        public Task<bool> AdminExists(GetAdmin admin, string UID);
    }
}
