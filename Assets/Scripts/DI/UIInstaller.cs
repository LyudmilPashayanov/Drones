using UI.ControlPanel;
using UI.Drones;
using UI.Jobs;
using UI.ViewModels;
using VContainer;
using VContainer.Unity;

namespace DI
{
    public static class UIInstaller
    {
        public static void Install(IContainerBuilder builder)
        {
            builder.Register<DronesViewModel>(Lifetime.Singleton);
            builder.Register<JobsViewModel>(Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<DroneListView>();
            builder.RegisterComponentInHierarchy<JobsListView>();
            builder.RegisterComponentInHierarchy<ControlPanelView>();
        }
    }
}