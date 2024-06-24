using Muchachos.TideTurner.Runtime.Boot.Initializers;
using Muchachos.TideTurner.Runtime.Level.FloatingObjects;
using Muchachos.TideTurner.Runtime.Level.LevelFsm;
using Muchachos.TideTurner.Runtime.Level.Obstacles;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private Barrel _prefab;

        [SerializeField]
        private Transform _barrelParent;

        [SerializeField]
        private Level.Level _level;
        
        public override void InstallBindings()
        {
            BindLevelUpdater();

            BindLevelStateFactory();
            BindLevelStateMachine();

            BindBarrelFactory();
            
            BindLevelInitializer();
        }

        private void BindBarrelFactory()
        {
            Container
                .Bind<IBarrelFactory>()
                .To<BarrelFactory>()
                .AsSingle()
                .WithArguments(_prefab, _barrelParent, _level);
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