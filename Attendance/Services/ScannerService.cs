using AttendanceAPI.Models;
using AttendanceAPI.Controllers;
using AttendanceAPI.EF;
using Microsoft.EntityFrameworkCore;
using AttendanceAPI.EF.DBO;
using AttendanceAPI.Interfaces;

namespace AttendanceAPI.Services
{
    public class ScannerService : IScanner
    {
        private readonly AttendanceContext _db;

        public ScannerService(AttendanceContext db)
        {
            _db = db;
        }
        public async Task<ScannerDBO> GetScanner(uint id)
        {
            return await _db.Scanners.FirstOrDefaultAsync(x => x.ScannerId == id);
        }

        public async Task<ScannerDBO> AddScanner(Scanner addscanner)
        {
            var newScanner = new ScannerDBO
            {
                ScannerName = addscanner.ScannerName,
                isActive = addscanner.isActive
            };
            _db.Scanners.Add(newScanner);
            await _db.SaveChangesAsync();
            return newScanner;
        }
        /*
        public async Task<ScannerDBO> UpdateScanner(ScannerDBO scanner)
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
        */
        public async Task<ScannerDBO> SetScannerStatus(uint id, bool isactive)
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
