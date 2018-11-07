using EditModule.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Collections.Generic;
using System.Linq;
using ToDoList.Data.Services;
using ToDoList.Infrastructure;

namespace EditModule.ViewModels
{
    public class ToolbarViewModel : BindableBase
    {
        private IEntryRepository _repository;
        private IRegionManager _regionManager;
        private IEnumerable<string> _categories;
        private IEventAggregator _eventAggregator;

        public DelegateCommand AddNewEntryCommand { get; set; }

        public IEnumerable<string> Categories
        {
            get { return _categories; }
            set { SetProperty(ref _categories, value); }
        }

        public ToolbarViewModel(IEntryRepository repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;

            AddNewEntryCommand = new DelegateCommand(AddNewEntry);

            Initialize();
        }

        private async void Initialize()
        {
            var repositoryList = await _repository.GetEntriesAsync();
            var categoryList = repositoryList.Select(e => e.Category)
                                             .Distinct();
            Categories = categoryList;
        }

        private void AddNewEntry()
        {
            _regionManager.RequestNavigate(RegionNames.UserInteractionRegion,
                typeof(EntryEditView).FullName);
        }
    }
}
