using VContainer;
using VContainer.Unity;

namespace DI
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            GameplayInstaller.Install(builder);
            SimulationInstaller.Install(builder);
            UIInstaller.Install(builder);
        }
    }
}