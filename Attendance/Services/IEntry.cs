using Attendance.Models;

namespace Attendance.Services
{
    public interface IEntry
    {
        public Task<List<Entry>> GetEntries();
        public Task<List<Entry>> GetEntriesByClientId(uint clientid);
        public Task<List<Entry>> GetEntriesByCourseId(uint clientid);
        public Task<Entry> AddEntry(UpdateEntry addentry);
        public Task<Entry> UpdateEntry(Entry entry);
        public Task<bool> DeleteEntry(int id);
    }
}
