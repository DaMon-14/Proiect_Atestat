using Attendance.Models;

namespace Attendance.Services
{
    public interface IEntry
    {
        public Task<Entry> AddEntry(UpdateEntry addentry);
    }
}
