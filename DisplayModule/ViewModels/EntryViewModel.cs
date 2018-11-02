using Prism.Mvvm;
using System;
using System.Collections.Generic;
using ToDoList.Data.Models;
using ToDoList.Data.Services;

namespace DisplayModule.ViewModels
{
    public class EntryViewModel : BindableBase
    {
        #region TestData
        List<Entry> _testList = new List<Entry>()
        {
            new Entry
            {
                Name = "Finish app",
                Description = "Complete the construction of this application.",
                IsCompleted = false,
                DateAdded = DateTime.Now,
                DueDate = DateTime.Now.AddDays(5),
                Id = Guid.NewGuid(),
                SubEntries = new List<SubEntry>()
                {
                    new SubEntry
                    {
                        Name = "Make coffee",
                        Description = "Fuel code via caffeine and stay up late as fuck",
                        IsCompleted = false,
                        DateAdded = DateTime.Now,
                        DueDate = DateTime.Now.AddHours(5),
                        Id = Guid.NewGuid(),
                        SubEntries = null,
                    },

                    new SubEntry
                    {
                        Name = "Make coffee",
                        Description = "Fuel code via caffeine late late late late late",
                        IsCompleted = true,
                        DateAdded = DateTime.Now,
                        DueDate = DateTime.Now.AddHours(8),
                        Id = Guid.NewGuid(),
                        SubEntries = null
                    }
                }
            },

            new Entry
            {
                Name = "Make coffee",
                Description = "Fuel code via caffeine",
                IsCompleted = false,
                DateAdded = DateTime.Now,
                DueDate = DateTime.Now.AddHours(5),
                Id = Guid.NewGuid(),
                SubEntries = null
            }
        };

        public List<Entry> TestList
        {
            get { return _testList; }
            set { SetProperty(ref _testList, value); }
        }
        #endregion

        IEntryRepository _repository;
        public EntryViewModel(IEntryRepository repository)
        {
            _repository = repository;
        }
    }
}
