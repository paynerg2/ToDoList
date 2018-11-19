using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using ToDoList.Data.Models;
using ToDoList.Data.Services;
using ToDoList.Infrastructure;
using Unity;
using System.Linq;

namespace DisplayModule.ViewModels
{
    
    public class EntryViewModel : BindableBase, INavigationAware
    {
        private IUnityContainer _container;
        private IEntryRepository _repository;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private Entry _selectedEntry;
        private List<Entry> _entries;
        private bool _editSectionIsVisible = false;

        public DelegateCommand CompleteCommand { get; set; }
        public DelegateCommand EditCommand { get; set; }
        public DelegateCommand DeleteCommand { get; set; }
        
        public Entry SelectedEntry
        {
            get { return _selectedEntry; }
            set { SetProperty(ref _selectedEntry, value); }
        }

        public List<Entry> Entries
        {
            get { return _entries; }
            set { SetProperty(ref _entries, value); }
        }
        
        public bool EditSectionIsVisible
        {
            get { return _editSectionIsVisible; }
            set { SetProperty(ref _editSectionIsVisible, value); }
        }


        public EntryViewModel(IUnityContainer container, IRegionManager regionManager,
            IEventAggregator eventAggregator)
        {
            _container = container;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            CompleteCommand = new DelegateCommand(Complete);
            DeleteCommand = new DelegateCommand(Delete);
            EditCommand = new DelegateCommand(Edit);

            _eventAggregator.GetEvent<CategoryFilterEvent>().Subscribe(FilterByCategory);

            Initialize();
        }

        private async void FilterByCategory(string category)
        {
            //_repository = _container.Resolve<IEntryRepository>();
            var fullEntryList = await _repository.GetEntriesAsync();
            Entries = category == string.Empty ? fullEntryList : 
                fullEntryList.Where(e => e.Category == category).ToList();
        }

        private void Edit()
        {
            var param = new NavigationParameters
            {
                {"Entry", _selectedEntry }
            };
            _regionManager.RequestNavigate(RegionNames.UserInteractionRegion, "EntryEditView", param);
        }

        private async void Delete()
        {
            await _repository.DeleteEntryAsync(SelectedEntry.Id);
            Initialize();
        }

        private void Complete()
         {
            SelectedEntry.IsCompleted = !SelectedEntry.IsCompleted;
        }

        private async void Initialize()
        {
            _repository = _container.Resolve<IEntryRepository>();
            Entries = await _repository.GetEntriesAsync();
        }

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Initialize();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
        #endregion
    }
}
