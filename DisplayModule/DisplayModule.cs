using DisplayModule.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace DisplayModule
{
    public class DisplayModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<EntryView>();
        }
    }
}
