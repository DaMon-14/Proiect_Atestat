using AttendanceAPI.Models;

namespace AttendanceAPI.Services
{
    public interface IScanner
    {
        Task<List<Scanner>> GetScanners();
        Task<Scanner> GetScanner(uint id);
        Task<Scanner> AddScanner(UpdateScanner scanner);
        Task<Scanner> UpdateScanner(uint id, UpdateScanner scanner);
        Task<Scanner> SetScannerStatus(uint id, bool isactive);
    }
}
