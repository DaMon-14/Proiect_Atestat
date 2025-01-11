using Prezenta_API.Models;

namespace Prezenta_API.Services
{
    public interface IEntry
    {
        List<Entry> GetAllEntries();
        Entry GetEntryByUserId(int userId);
        Entry AddEntry(UpdateEntry entry);
        Entry UpdateEntry(int id, UpdateEntry entry);
        bool DeleteEntry(int id);
    }
}
