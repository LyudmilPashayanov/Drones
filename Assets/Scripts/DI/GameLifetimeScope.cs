using Pathfinding;
using UI.Drones;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        // TODO : make different methods depending on what you are registering:) 
        // TODO : Make interfaces for all classes so I can replace them if I want to.
        
        builder.Register<WorldGrid>(Lifetime.Singleton);
        builder.Register<TrafficController>(Lifetime.Singleton);
        builder.Register<StepCoordinator>(Lifetime.Singleton);
        
        // View Models
        builder.Register<DronesViewModel>(Lifetime.Singleton);
        builder.Register<JobsViewModel>(Lifetime.Singleton);
        
        builder.Register<IPathfinder, DiagonalPathfinder>(Lifetime.Singleton);
        
        builder.RegisterComponentInHierarchy<WorldGenerator>();
        
        builder.RegisterComponentInHierarchy<DroneTester>();
        
        builder.RegisterComponentInHierarchy<DroneFactory>();
        
        builder.RegisterComponentInHierarchy<DroneListView>();
        builder.RegisterComponentInHierarchy<JobsListView>();
        builder.RegisterComponentInHierarchy<ControlPanelView>();
    }
}
