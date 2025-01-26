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
        private readonly IConfiguration _configuration;

        public AttendanceService(AttendanceContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        public async Task<AttendanceDBO> AddEntry(Attendance addentry, string UID)
        {
            if(UID != _configuration.GetValue<string>("UID"))
            {
                return null;
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
            return newEntry;
        }

        public async Task<List<AttendanceDBO>> GetEntries(string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            return await _db.Entries.Where(x=>x.Id>0).ToListAsync();
        }

        public async Task<AttendanceDBO> GetEntriesById(uint Id, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            return await _db.Entries.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<List<AttendanceDBO>> GetEntriesByClientId(uint clientId, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            return await _db.Entries.Where(x => x.ClientId == clientId && x.Id>0).ToListAsync();
        }

        public async Task<List<AttendanceDBO>> GetEntriesByCourseId(uint courseId, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            return await _db.Entries.Where(x => x.CourseId == courseId && x.Id>0).ToListAsync();
        }

        public async Task<AttendanceDBO> UpdateEntry(AttendanceDBO entry, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return null;
            }
            var updateEntry = await _db.Entries.FirstOrDefaultAsync(x => x.Id == entry.Id);
            if (updateEntry == null)
            {
                return null;
            }
            updateEntry.ClientId = entry.ClientId;
            updateEntry.CourseId = entry.CourseId;
            updateEntry.ScanTime = entry.ScanTime;
            await _db.SaveChangesAsync();
            return updateEntry;
        }

        public async Task<bool> DeleteEntry(int id, string UID)
        {
            if (UID != _configuration.GetValue<string>("UID"))
            {
                return false;
            }
            var entry = await _db.Entries.FirstOrDefaultAsync(x => x.Id == id);
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
