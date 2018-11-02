using System;

namespace ToDoList.Data.Models
{
    public class SubEntry : Entry
    {
        public Entry ParentEntry { get; set; }
        public Guid ParentEntryId { get; set; }
    }
}
