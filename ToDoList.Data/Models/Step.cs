using System;

namespace ToDoList.Data.Models
{
    public class Step
    {
        public Guid Id { get; set; }
        public Guid EntryId { get; set; }
        public string Data { get; set; }
    }
}
