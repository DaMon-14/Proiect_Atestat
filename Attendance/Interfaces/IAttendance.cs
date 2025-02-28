﻿using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IAttendance
    {
        public Task<List<AttendanceDBO>> GetEntries();
        public Task<AttendanceDBO> GetEntryById(uint Id);
        public Task<string> AddEntry(Attendance addentry);
        public Task<string> UpdateEntry(AttendanceDBO entry);
        public Task<bool> DeleteEntry(int id);
    }
}
