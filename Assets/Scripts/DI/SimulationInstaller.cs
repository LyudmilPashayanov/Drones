using Pathfinding;
using VContainer;
using World;

namespace DI
{
    public static class SimulationInstaller
    {
        public static void Install(IContainerBuilder builder)
        {
            builder.Register<WorldGrid>(Lifetime.Singleton);
            builder.Register<TrafficController>(Lifetime.Singleton);
            builder.Register<StepCoordinator>(Lifetime.Singleton);

            builder.Register<IPathfinder, DiagonalPathfinder>(Lifetime.Singleton);
        }
    }
}