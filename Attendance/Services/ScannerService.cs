using Attendance.Models;
using Attendance.Controllers;
using Attendance.EF;
using Microsoft.EntityFrameworkCore;

namespace Attendance.Services
{
    public class ScannerService : IScanner
    {
        private readonly AttendanceContext _db;

        public ScannerService(AttendanceContext db)
        {
            _db = db;
        }

        public async Task<List<Scanner>> GetScanners()
        {
            return await _db.Scanners.Where(x => x.ScannerId > 0).ToListAsync();
        }

        public async Task<Scanner> GetScanner(uint id)
        {
            return await _db.Scanners.FirstOrDefaultAsync(x => x.ScannerId == id);
        }

        public async Task<Scanner> AddScanner(UpdateScanner addscanner)
        {
            var newScanner = new Scanner
            {
                ScannerName = addscanner.ScannerName,
                isActive = addscanner.isActive
            };
            _db.Scanners.Add(newScanner);
            await _db.SaveChangesAsync();
            return newScanner;
        }

        public async Task<Scanner> UpdateScanner(uint id, UpdateScanner scanner)
        {
            var scannerToUpdate = await _db.Scanners.FirstOrDefaultAsync(x => x.ScannerId == id);
            if (scannerToUpdate == null)
            {
                return null;
            }

            scannerToUpdate.ScannerName = scanner.ScannerName;
            scannerToUpdate.isActive = scanner.isActive;

            await _db.SaveChangesAsync();
            return scannerToUpdate;
        }

        public async Task<Scanner> SetScannerStatus(uint id, bool isactive)
        {
            var scannerToUpdate = await _db.Scanners.FirstOrDefaultAsync(x => x.ScannerId == id);
            if (scannerToUpdate == null)
            {
                return null;
            }

            scannerToUpdate.isActive = isactive;

            await _db.SaveChangesAsync();
            return scannerToUpdate;
        }
    }
}
