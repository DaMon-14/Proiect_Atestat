using Microsoft.EntityFrameworkCore;
using Prezenta_API.EF;
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
            return await _db.Entries.Where(x=>x.Id >0).ToListAsync();
        }

        public async Task<Entry> GetEntryById(uint id)
        {
            return await _db.Entries.FirstOrDefaultAsync(entry => entry.Id == id);
        }

        public async Task<Entry> GetEntryByUserCode(uint id)
        {
            return await _db.Entries.FirstOrDefaultAsync(entry => entry.UserCode == id);
        }

        public async Task<Entry> AddEntry(UpdateEntry entry) 
        {
            var addentry = new Entry()
            {
                UserCode = entry.UserCode,
                ScanTime = entry.ScanTime,
                ScannerId = entry.ScannerId,
            };
            _db.Add(addentry);
            var result = await _db.SaveChangesAsync();
            return result >= 0 ? addentry : null;
        }

        public async Task<Entry> UpdateEntry(uint id, UpdateEntry entryinfo)
        { 
            var Entry = await _db.Entries.FirstOrDefaultAsync(index => index.Id == id);
            if (Entry != null) 
            {
                Entry.UserCode = entryinfo.UserCode;
                Entry.ScanTime = entryinfo.ScanTime;
                Entry.ScannerId = entryinfo.ScannerId;
                var result = await _db.SaveChangesAsync();
                return result >= 0 ? Entry : null;
            }
                return null;
        }

        public async Task<bool> DeleteEntry(uint id) 
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
