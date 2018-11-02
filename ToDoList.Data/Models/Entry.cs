using System;
using System.Collections.Generic;

namespace ToDoList.Data.Models
{
    public class Entry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime? DueDate { get; set; }
        public Guid Id { get; set; }

        public ICollection<SubEntry> SubEntries { get; set; }
        
    }
}
