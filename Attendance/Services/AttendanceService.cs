using AttendanceAPI.EF;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;
using AttendanceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceAPI.Services
{
    public class AttendanceService : IAttendance
    {
        private readonly AttendanceContext _db;

        public AttendanceService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<string> AddEntry(Attendance addentry)
        {
            if(addentry == null)
            {
                return "error";
            }
            if(_db.Courses.FirstOrDefault(x=>x.CourseId == addentry.CourseId) == null)
            {
                return "Course not found";
            }
            if (_db.Users.FirstOrDefault(x=>x.ClientId == addentry.ClientId && x.IsAdmin==false) == null)
            {
                return "Client not found";
            }
            var newEntry = new AttendanceDBO
            {
                ClientId = addentry.ClientId,
                CourseId = addentry.CourseId,
            };
            if (addentry.ScanTime == new DateTime(1, 1, 1, 1, 1, 1, DateTimeKind.Utc))
            {
                newEntry.ScanTime = DateTime.Now;
            }
            else
            {
                newEntry.ScanTime = addentry.ScanTime;
            }
            _db.Entries.Add(newEntry);
            await _db.SaveChangesAsync();
            return "Ok";
        }

        public async Task<List<AttendanceDBO>> GetEntries()
        {
            return await _db.Entries.Where(x=>x.Id>0).ToListAsync();
        }

        public async Task<AttendanceDBO> GetEntryById(uint Id)
        {
            return await _db.Entries.FirstOrDefaultAsync(x => x.Id == Id && x.Id > 0);
        }

        public async Task<AttendanceDBO> UpdateEntry(AttendanceDBO entry)
        {
            var updateEntry = await _db.Entries.FirstOrDefaultAsync(x => x.Id == entry.Id && x.Id>0);
            if (updateEntry == null)
            {
                return null;
            }
            updateEntry.CourseId = entry.CourseId;
            updateEntry.ScanTime = entry.ScanTime;
            await _db.SaveChangesAsync();
            return updateEntry;
        }

        public async Task<bool> DeleteEntry(int id)
        {
            var entry = await _db.Entries.FirstOrDefaultAsync(x => x.Id == id && x.Id > 0);
            if (entry == null)
            {
                return false;
            }
            _db.Entries.Remove(entry);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
