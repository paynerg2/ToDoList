using EditModule.Views;
using Prism.Commands;
using Prism.Regions;
using ToDoList.Data.Services;
using ToDoList.Infrastructure;

namespace EditModule.ViewModels
{
    public class ToolbarViewModel
    {
        private IEntryRepository _repository;
        private IRegionManager _regionManager;

        public DelegateCommand AddNewEntryCommand { get; set; }

        public ToolbarViewModel(IEntryRepository repository, IRegionManager regionManager)
        {
            _repository = repository;
            _regionManager = regionManager;

            AddNewEntryCommand = new DelegateCommand(AddNewEntry);
        }

        private void AddNewEntry()
        {
            _regionManager.RequestNavigate(RegionNames.UserInteractionRegion,
                typeof(EntryEditView).FullName);
        }
    }
}
