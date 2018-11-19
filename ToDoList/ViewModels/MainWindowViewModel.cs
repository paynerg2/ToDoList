using DisplayModule.Views;
using EditModule.Views;
using Prism.Mvvm;
using Prism.Regions;
using ToDoList.Infrastructure;

namespace ToDoList.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;

            Initialize();
        }

        private void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.EntryRegion, typeof(EntryView));
            _regionManager.RegisterViewWithRegion(RegionNames.UserInteractionRegion, typeof(ToolbarView));
            _regionManager.RegisterViewWithRegion(RegionNames.UserInteractionRegion, typeof(EntryEditView));
        }
    }
}
