using EditModule.Views;
using DisplayModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Data.Entity;
using System.Windows;
using ToDoList.Data;
using ToDoList.Data.Services;
using ToDoList.Views;
using Unity.Lifetime;
using Unity;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.GetContainer().RegisterType<DbContext, EntryDbContext>(new PerResolveLifetimeManager());
            containerRegistry.GetContainer().RegisterType<IEntryRepository, EntryRepository>(new PerResolveLifetimeManager());
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<EditModule.EditModule>();
            moduleCatalog.AddModule<DisplayModule.DisplayModule>();
        }
    }
}
