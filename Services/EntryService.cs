using Microsoft.EntityFrameworkCore;
using Prezenta_API.Entity;
using Prezenta_API.Models;
using System.Diagnostics.Contracts;
using System.Diagnostics.Eventing.Reader;

namespace Prezenta_API.Services
{
    public class EntryService : IEntry
    {
        private readonly EntryContext _db;
        public EntryService(EntryContext db)
        {
            _db = db;
        }

        public async Task<List<Entry>> GetAllEntries()
        {
            return await _db.Entries.ToListAsync();
        }

        public async Task<Entry> GetEntryByUserId(int id)
        {
            return await _db.Entries.FirstOrDefaultAsync(entry => entry.Id == id);
        }

        public async Task<Entry> AddEntry(UpdateEntry entry) 
        {
            var addentry = new Entry()
            {
                ScanTime = entry.ScanTime
            };
            _db.Add(addentry);
            var result = await _db.SaveChangesAsync();
            return result >= 0 ? addentry : null;
        }

        public async Task<Entry> UpdateEntry(int id, UpdateEntry entryinfo)
        { 
            var Entry = await _db.Entries.FirstOrDefaultAsync(index => index.Id == id);
            if (Entry != null) 
            {
                Entry.ScanTime = entryinfo.ScanTime;
                var result = await _db.SaveChangesAsync();
                return result >= 0 ? Entry : null;
            }
                return null;
        }

        public async Task<bool> DeleteEntry(int id) 
        { 
            var Entry = await _db.Entries.FirstOrDefaultAsync(index => index.Id == id);
            if (Entry != null)
            {
                _db.Entries.Remove(Entry);
                var result = await _db.SaveChangesAsync();
                return result >= 0;
            }
            return false;
        }

    }
}
