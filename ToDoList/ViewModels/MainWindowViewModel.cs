using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using ToDoList.Views;

namespace ToDoList.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        IRegionManager _regionManager;
        IContainerExtension _container;

        public MainWindowViewModel(IContainerExtension container, IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _container = container;

            Initialize();
        }

        private void Initialize()
        {
            _regionManager.RegisterViewWithRegion(RegionNames.EntryRegion, typeof(EntryView));
            _regionManager.RegisterViewWithRegion(RegionNames.UserInteractionRegion, typeof(EntryEditView));
            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, typeof(ToolbarView));
        }
    }
}
