using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Data.Models;

namespace ToDoList.Data.Services
{
    public class EntryRepository : IEntryRepository
    {
        EntryDbContext _context;

        public EntryRepository(EntryDbContext context)
        {
            _context = context;
        }

        public async Task<Entry> AddEntryAsync(Entry entry)
        {
            _context.Entries.Add(entry);
            await _context.SaveChangesAsync();
            return entry;
        }
        
        public async Task<List<Entry>> GetEntriesAsync()
        {
            return await _context.Entries
                                 .OrderBy(e => e.DateAdded)
                                 .ToListAsync<Entry>();
        }

        public async Task<Entry> GetEntryAsync(Guid entryId)
        {
            return await _context.Entries.FirstOrDefaultAsync(e => e.Id == entryId);
        }

        public async Task<Entry> UpdateEntryAsync(Entry entry)
        {
            if(!_context.Entries.Local.Any(e => e.Id == entry.Id))
            {
                _context.Entries.Attach(entry);
            }
            _context.Entry(entry).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entry;
        }

        public async Task DeleteEntryAsync(Guid entryId)
        {
            var entry = _context.Entries.FirstOrDefault(e => e.Id == entryId);
            if (entry != null)
                _context.Entries.Remove(entry);
            await _context.SaveChangesAsync();

        }
    }
}
