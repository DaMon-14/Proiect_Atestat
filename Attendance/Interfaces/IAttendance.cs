using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IAttendance
    {
        public Task<List<AttendanceDBO>> GetEntries();
        public Task<AttendanceDBO> GetEntryById(uint Id);
        public Task<AttendanceDBO> AddEntry(Attendance addentry);
        public Task<AttendanceDBO> UpdateEntry(AttendanceDBO entry);
        public Task<bool> DeleteEntry(int id);
    }
}
