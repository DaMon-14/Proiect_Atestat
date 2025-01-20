using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Services
{
    public interface IAdmin
    {
        public Task<AdminDBO> AddAdmin(Admin addAdmin, string UID);
        public Task<AdminDBO> UpdateAdmin(AdminDBO admin, string UID);
        public Task<bool> AdminExists(GetAdmin admin, string UID);
    }
}
