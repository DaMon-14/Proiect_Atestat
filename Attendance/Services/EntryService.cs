using Attendance.EF;
using Attendance.Models;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Services
{
    public class EntryService : IEntry
    {
        private readonly AttendanceContext _db;

        public EntryService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<Entry> AddEntry(UpdateEntry addentry)
        {
            var newEntry = new Entry
            {
                ClientId = addentry.ClientId,
                CourseId = addentry.CourseId,
                ScanTime = DateTime.Now
            };
            _db.Entries.Add(newEntry);
            await _db.SaveChangesAsync();
            return newEntry;
        }
    }
}
