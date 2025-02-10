using AttendanceAPI.EF.DBO;
using AttendanceAPI.Models;

namespace AttendanceAPI.Interfaces
{
    public interface IScanner
    {
        Task<List<ScannerDBO>> GetAllScanners();
        Task<ScannerDBO> GetScanner(uint id);
        Task<ScannerDBO> AddScanner(Scanner scanner);
        Task<ScannerDBO> UpdateScanner(ScannerDBO scanner);
        Task<bool> DeleteScanner(uint id);
    }
}
