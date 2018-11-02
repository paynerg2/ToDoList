using EditModule.Views;
using Prism.Ioc;
using Prism.Modularity;
using ToDoList.Infrastructure;

namespace EditModule
{
    [Module(ModuleName = ModuleNames.EditModule)]
    public class EditModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ToolbarView>();
            containerRegistry.RegisterForNavigation<EntryEditView>();
        }
    }
}
