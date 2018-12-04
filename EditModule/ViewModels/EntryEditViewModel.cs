using DisplayModule.Views;
using EditModule.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using ToDoList.Data.Models;
using ToDoList.Data.Services;
using ToDoList.Infrastructure;

namespace EditModule.ViewModels
{
    public class EntryEditViewModel : BindableBase, INavigationAware
    {
        private string _name;
        private DateTime? _dueDate = DateTime.Now;
        private string _description;
        private ICollection<Step> _subEntries;
        private IEntryRepository _repository;
        private IRegionManager _regionManager;
        private static Entry _selectedEntry;
        private bool _isInEditMode = false;
        private string _category;


        public string Name
        {
            get { return _name; }
            set
            {
                SetProperty(ref _name, value);
                if(_selectedEntry != null)
                    _selectedEntry.Name = Name;
            }
        }

        public DateTime? DueDate
        {
            get { return _dueDate; }
            set
            {
                SetProperty(ref _dueDate, value);
                if(_selectedEntry != null)
                    _selectedEntry.DueDate = DueDate;
            }
        }

        public string Description
        {
            get { return _description; }
            set
            {
                SetProperty(ref _description, value);
                if(_selectedEntry != null)
                    _selectedEntry.Description = Description;
            }
        }

        public ICollection<Step> SubEntries
        {
            get { return _subEntries; }
            set
            {
                SetProperty(ref _subEntries, value);
                if(_selectedEntry != null)
                    _selectedEntry.Steps = SubEntries;
            }
        }
        
        public string Category
        {
            get { return _category; }
            set
            {
                SetProperty(ref _category, value);
                if (_selectedEntry != null)
                    _selectedEntry.Category = Category;
            }
        }

        public Entry SelectedEntry
        {
            get { return _selectedEntry; }
            set { SetProperty(ref _selectedEntry, value); }
        }
        
        public DelegateCommand SaveCommand { get; set; }
        public DelegateCommand NavigateBackCommand { get; set; }
        public DelegateCommand AddStepCommand { get; set; }

        public EntryEditViewModel(IEntryRepository repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;
            
            NavigateBackCommand = new DelegateCommand(NavigateToHome);
            SaveCommand = new DelegateCommand(Save);
            AddStepCommand = new DelegateCommand(AddStep);
        }

        private void AddStep()
        {
            if (SubEntries == null)
                SubEntries = new List<Step>();
            SubEntries.Add(new Step() { Data = "new step" });
        }

        private void Save()
        {
            if(_isInEditMode)
                _repository.UpdateEntryAsync(_selectedEntry);
            else
                _repository.AddEntryAsync(_selectedEntry);

            NavigateToHome();
        }

        private void NavigateToHome()
        {
            _regionManager.RequestNavigate(RegionNames.UserInteractionRegion, typeof(ToolbarView).FullName);
            _regionManager.RequestNavigate(RegionNames.EntryRegion, typeof(EntryView).FullName);
        }

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _isInEditMode = false;

            if (navigationContext.Parameters["Entry"] is Entry entryToEdit)
            {
                SelectedEntry = entryToEdit;

                // Update display fields
                Name = SelectedEntry.Name;
                Description = SelectedEntry.Description;
                SubEntries = SelectedEntry.Steps;
                DueDate = SelectedEntry.DueDate;

                _isInEditMode = true;
            }
                
            if (SelectedEntry == null)
            {
                SelectedEntry = new Entry()
                {
                    Id = Guid.NewGuid(),
                    DateAdded = DateTime.Now,
                    DueDate = DateTime.Now,
                    IsCompleted = false
                };
            }
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            // Logic to determine if this should be a new instance of the view
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            ClearData();
        }
        #endregion

        private void ClearData()
        {
            Name = string.Empty;
            Description = string.Empty;
            SubEntries = null;
            SelectedEntry = null;
        }
    }
}
