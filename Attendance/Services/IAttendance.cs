using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Services
{
    public interface IAttendance
    {
        public Task<List<AttendanceDBO>> GetEntries();
        public Task<List<AttendanceDBO>> GetEntriesByClientId(uint clientid);
        public Task<List<AttendanceDBO>> GetEntriesByCourseId(uint clientid);
        public Task<AttendanceDBO> AddEntry(Attendance addentry);
        public Task<AttendanceDBO> UpdateEntry(AttendanceDBO entry);
        public Task<bool> DeleteEntry(int id);
    }
}
