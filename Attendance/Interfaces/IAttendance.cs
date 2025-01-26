using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IAttendance
    {
        public Task<List<AttendanceDBO>> GetEntries(string UID);
        public Task<AttendanceDBO> GetEntriesById(uint Id, string UID);
        public Task<List<AttendanceDBO>> GetEntriesByClientId(uint clientid, string UID);
        public Task<List<AttendanceDBO>> GetEntriesByCourseId(uint clientid, string UID);
        public Task<AttendanceDBO> AddEntry(Attendance addentry, string UID);
        public Task<AttendanceDBO> UpdateEntry(AttendanceDBO entry, string UID);
        public Task<bool> DeleteEntry(int id, string UID);
    }
}
