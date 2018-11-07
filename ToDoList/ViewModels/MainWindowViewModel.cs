﻿using DisplayModule.Views;
using EditModule.Views;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Regions;
using ToDoList.Infrastructure;

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
            _regionManager.RegisterViewWithRegion(RegionNames.UserInteractionRegion, typeof(ToolbarView));
            _regionManager.RegisterViewWithRegion(RegionNames.UserInteractionRegion, typeof(EntryEditView));
        }
    }
}
