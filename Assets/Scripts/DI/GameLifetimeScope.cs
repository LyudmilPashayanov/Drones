using Pathfinding;
using UI.Drones;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<WorldGrid>(Lifetime.Singleton);
        builder.Register<TrafficController>(Lifetime.Singleton);
        builder.Register<StepCoordinator>(Lifetime.Singleton);
        builder.Register<DronesViewModel>(Lifetime.Singleton);
        builder.Register<JobsViewModel>(Lifetime.Singleton);
        
        builder.Register<IPathfinder, DiagonalPathfinder>(Lifetime.Singleton);
        
        builder.RegisterComponentInHierarchy<WorldGenerator>();
        builder.RegisterComponentInHierarchy<DroneTester>();
        builder.RegisterComponentInHierarchy<DroneFactory>();
        builder.RegisterComponentInHierarchy<DroneListView>();

    }
}
