using EditModule.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.Services;
using ToDoList.Infrastructure;

namespace EditModule.ViewModels
{
    public class ToolbarViewModel : BindableBase, INavigationAware
    {
        private IEntryRepository _repository;
        private IRegionManager _regionManager;
        private IEnumerable<string> _categories;
        private IEventAggregator _eventAggregator;
        private string _selectedCategory;

        public DelegateCommand AddNewEntryCommand { get; set; }

        public IEnumerable<string> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        public string SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                SetProperty(ref _selectedCategory, value);
                FilterByCategory();
            }
        }


        public ToolbarViewModel(IEntryRepository repository, IRegionManager regionManager,
            IEventAggregator eventAggregator)
        {
            _repository = repository;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            AddNewEntryCommand = new DelegateCommand(AddNewEntry);

            Initialize();
        }

        private void FilterByCategory()
        {
            _eventAggregator.GetEvent<CategoryFilterEvent>().Publish(SelectedCategory);
        }

        private async void Initialize()
        {
            var repositoryList = await _repository.GetEntriesAsync();
            List<string> categoryList = new List<string>() { string.Empty };
            var entryList = repositoryList.Select(e => e.Category)
                                             .Distinct();
            categoryList.AddRange(entryList);
            Categories = categoryList;
        }

        private void AddNewEntry()
        {
            _regionManager.RequestNavigate(RegionNames.UserInteractionRegion,
                typeof(EntryEditView).FullName);
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
