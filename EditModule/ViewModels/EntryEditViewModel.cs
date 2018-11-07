using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using ToDoList.Data.Models;
using ToDoList.Data.Services;

namespace EditModule.ViewModels
{
    public class EntryEditViewModel : BindableBase, INavigationAware
    {
        private string _name;
        private DateTime _dueDate = DateTime.Now;
        private string _description;
        private ICollection<Step> _subEntries;
        private IEntryRepository _repository;
        private static Entry _selectedEntry;


        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                _selectedEntry.Name = Name;
            }
        }

        public DateTime DueDate
        {
            get { return _dueDate; }
            set
            {
                SetProperty(ref _dueDate, value);
                _selectedEntry.DueDate = DueDate;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
                _selectedEntry.Description = Description;
            }
        }

        public ICollection<Step> SubEntries
        {
            get { return _subEntries; }
            set
            {
                SetProperty(ref _subEntries, value);
                _selectedEntry.Steps = SubEntries;
            }
        }
        

        public Entry SelectedEntry
        {
            get { return _selectedEntry; }
            set { SetProperty(ref _selectedEntry, value); }
        }
        
        public DelegateCommand SaveCommand { get; set; }

        public EntryEditViewModel(IEntryRepository repository)
        {
            _repository = repository;
            if(SelectedEntry == null)
            {
                SelectedEntry = new Entry()
                {
                    Id = Guid.NewGuid(),
                    DateAdded = DateTime.Now,
                    DueDate = DateTime.Now,
                    IsCompleted = false
                };
            }

            SaveCommand = new DelegateCommand(Save);
        }

        private void Save()
        {
            _repository.AddEntryAsync(_selectedEntry);
        }

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters["Entry"] is Entry entryToEdit)
                SelectedEntry = entryToEdit;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // Logic to determine if this should be a new instance of the view
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        #endregion

    }
}
