using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Services
{
    public interface IScanner
    {
        Task<ScannerDBO> GetScanner(uint id);
        Task<ScannerDBO> AddScanner(Scanner scanner);
        //Task<ScannerDBO> UpdateScanner(ScannerDBO scanner);
        Task<ScannerDBO> SetScannerStatus(uint id, bool isactive);
    }
}
