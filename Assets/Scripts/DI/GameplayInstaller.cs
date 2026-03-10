using Core;
using VContainer;
using VContainer.Unity;
using World;

namespace DI
{
    public static class GameplayInstaller
    {
        public static void Install(IContainerBuilder builder)
        {
            builder.RegisterComponentInHierarchy<DroneFactory>();
            builder.RegisterComponentInHierarchy<DroneTester>();
            builder.RegisterComponentInHierarchy<WorldGenerator>();
        }
    }
}