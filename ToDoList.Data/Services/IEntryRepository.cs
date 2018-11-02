using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Data.Models;

namespace ToDoList.Data.Services
{
    public interface IEntryRepository
    {
        Task<List<Entry>> GetEntriesAsync();
        Task<Entry> GetEntryAsync(Guid entryId);
        Task<Entry> AddEntryAsync(Entry entry);
        Task<Entry> UpdateEntryAsync(Entry entry);
        Task DeleteEntryAsync(Guid entryId);
    }
}
