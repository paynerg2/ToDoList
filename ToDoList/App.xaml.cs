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
            containerRegistry.RegisterSingleton<DbContext, EntryDbContext>();
            containerRegistry.Register<IEntryRepository, EntryRepository>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            moduleCatalog.AddModule<EditModule.EditModule>();
            moduleCatalog.AddModule<DisplayModule.DisplayModule>();
        }
    }
}
