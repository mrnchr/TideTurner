using Muchachos.TideTurner.Runtime.Boot.Initializers;
using Muchachos.TideTurner.Runtime.Level.LevelFsm;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLevelUpdater();

            BindLevelStateFactory();
            BindLevelStateMachine();
            
            BindLevelInitializer();
        }

        private void BindLevelStateFactory()
        {
            Container
                .Bind<ILevelStateFactory>()
                .To<LevelStateFactory>()
                .AsSingle();
        }

        private void BindLevelStateMachine()
        {
            Container
                .BindInterfacesAndSelfTo<LevelStateMachine>()
                .AsSingle();
        }

        private void BindLevelUpdater()
        {
            Container
                .BindInterfacesTo<LevelUpdater>()
                .AsSingle();
        }

        private void BindLevelInitializer()
        {
            Container
                .BindInterfacesTo<LevelInitializer>()
                .AsSingle();
        }
    }
}