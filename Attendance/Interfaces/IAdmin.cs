using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IAdmin
    {
        public Task<AdminDBO> AddAdmin(Admin addAdmin, string UID);
        public Task<AdminDBO> UpdateAdmin(AdminDBO admin, string UID);
        public Task<bool> AdminExists(Admin admin, string UID);
    }
}
