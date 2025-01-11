using Prezenta_API.Models;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;

namespace Prezenta_API.Services
{
    public class EntryService : IEntry
    {
        private readonly List<Entry> _entries;
        public EntryService()
        {
            _entries = new List<Entry>()
            {
                new Entry()
                {
                    Id = 1,
                    ScanTime = DateTime.Now
                }
            };
        }

        public List<Entry> GetAllEntries()
        {
            return _entries;
        }

        public Entry GetEntryByUserId(int id)
        {
            return _entries.FirstOrDefault(entry => entry.Id == id);
        }

        public Entry AddEntry(UpdateEntry entry) 
        {
            Entry addentry = new Entry()
            {
                Id = _entries.Max(entry => entry.Id) + 1,
                ScanTime = entry.ScanTime
            };
            _entries.Add(addentry);
            return addentry;
        }

        public Entry UpdateEntry(int id, UpdateEntry entryinfo)
        { 
            int EntryIndex = _entries.FindIndex(entry => entry.Id == id);
            if (EntryIndex > 0) 
            {
                Entry entry = _entries[EntryIndex];
                entry.ScanTime = entryinfo.ScanTime;
                _entries[EntryIndex] = entry;
                return entry;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteEntry(int id) 
        { 
            int EntryIndex = _entries.FindIndex(_entry => _entry.Id == id);
            if (EntryIndex > 0)
            { 
                _entries.RemoveAt(EntryIndex);
            }
            return EntryIndex >= 0;
        }

    }
}
