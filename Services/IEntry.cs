using Prezenta_API.Models;

namespace Prezenta_API.Services
{
    public interface IEntry
    {
        Task<List<Entry>> GetAllEntries();
        Task<Entry> GetEntryByUserId(int userId);
        Task<Entry> AddEntry(UpdateEntry entry);
        Task<Entry> UpdateEntry(int id, UpdateEntry entry);
        Task<bool> DeleteEntry(int id);
    }
}
