using Prezenta_API.Models;

namespace Prezenta_API.Services
{
    public interface IEntry
    {
        Task<List<Entry>> GetAllEntries();
        Task<Entry> GetEntryById(uint Id);
        Task<Entry> GetEntryByUserId(uint userId);
        Task<Entry> AddEntry(UpdateEntry entry);
        Task<Entry> UpdateEntry(uint id, UpdateEntry entry);
        Task<bool> DeleteEntry(uint id);
    }
}
