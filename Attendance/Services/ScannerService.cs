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
        public async Task<List<ScannerDBO>> GetAllScanners()
        {
            return await _db.Scanners.Where(x=>x.ScannerId>0).ToListAsync();
        }

        public async Task<ScannerDBO> GetScanner(uint id)
        {
            return await _db.Scanners.FirstOrDefaultAsync(x => x.ScannerId == id);
        }

        public async Task<ScannerDBO> AddScanner(Scanner addscanner)
        {
            if(addscanner.ScannerName == "")
            {
                return null;
            }
            var newScanner = new ScannerDBO
            {
                ScannerName = addscanner.ScannerName,
                isActive = addscanner.isActive
            };
            _db.Scanners.Add(newScanner);
            await _db.SaveChangesAsync();
            return newScanner;
        }
        
        public async Task<ScannerDBO> UpdateScanner(ScannerDBO scanner)
        {
            var scannerToUpdate = await _db.Scanners.FirstOrDefaultAsync(x => x.ScannerId == scanner.ScannerId);
            if (scannerToUpdate == null)
            {
                return null;
            }
            if(scanner.ScannerName != "")
            {
                scannerToUpdate.ScannerName = scanner.ScannerName;
            }
            scannerToUpdate.isActive = scanner.isActive;

            await _db.SaveChangesAsync();
            return scannerToUpdate;
        }

        public async Task<bool> DeleteScanner(uint id)
        {
            var scannerToDelete = await _db.Scanners.FirstOrDefaultAsync(x => x.ScannerId == id);
            if (scannerToDelete == null)
            {
                return false;
            }
            _db.Scanners.Remove(scannerToDelete);
            await _db.SaveChangesAsync();
            return true;
        }
        
    }
}
