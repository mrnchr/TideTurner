using System.Linq;
using Muchachos.TideTurner.Runtime.Boot.Initializers;
using Muchachos.TideTurner.Runtime.Core.Input;
using Muchachos.TideTurner.Runtime.Level;
using Muchachos.TideTurner.Runtime.Level.FloatingObjects;
using Muchachos.TideTurner.Runtime.Level.LevelFsm;
using Muchachos.TideTurner.Runtime.Level.Obstacles;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Cannon;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Shark;
using Muchachos.TideTurner.Runtime.Level.Savings;
using Muchachos.TideTurner.Runtime.Mobile;
using Muchachos.TideTurner.Runtime.UI;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private Barrel _prefab;

        [SerializeField] private Transform _barrelParent;
        
        [Header("Moon")] 
        [SerializeField] private AbstractMoon _moon;
        [SerializeField] private AbstractMoonData _moonData;
        [Header("Essential")] 
        [SerializeField] private Level.Level _level;
        [SerializeField] private Water _water;
        [SerializeField] private WaterMovement _waterMovement;
        [SerializeField] private PauseWindow _pauseWindow;
        [SerializeField] private LoseWindow _loseWindow;
        [SerializeField] private SoundRestarter _soundRestarter;
        [SerializeField] private LevelFreezer _levelFreezer;
        [SerializeField] private CameraMovement _cameraMovement;
        [SerializeField] private MobileScreenOrientation _mobileScreenOrientation;
        [Header("CheckPoints")] 
        [SerializeField] private CheckPoint[] _checkPoints;
        [SerializeField] private CheckPointHandler _checkPointHandler;
        [Header("Boat")] 
        [SerializeField] private BoatSpawn _boatSpawn;
        [SerializeField] private Boat _boat;
        [Header("Containers")] 
        [SerializeField] private BarrelContainer _barrelContainer;
        [SerializeField] private SharkContainer _sharkContainer;
        [Header("Obstacles")] 
        [SerializeField] private BallPool _ballPool;
        [SerializeField] private Tentacle[] _tentacles;
        
        [SerializeField] private bool _isMobileControl;

        public override void InstallBindings()
        {
            BindInputController();
            BindInputHandler();
            
            Container.BindInstance(_soundRestarter).AsSingle();

            Container.BindInstance(_moon).AsSingle();
            Container.BindInstance(_moonData).AsSingle();
            
            Container.BindInstance(_waterMovement).AsSingle();
            Container.BindInstance(_water).AsSingle();
            
            Container.BindInstance(_boatSpawn).AsSingle();
            Container.BindInstance(_boat).AsSingle();
            
            Container.BindInstance(_cameraMovement).AsSingle();
            
            Container.BindInstance(_level).AsSingle();
            
            Container.BindInstance(_ballPool).AsSingle();
            
            Container.BindInstance(_sharkContainer).AsSingle();
            
            Container.BindInstance(_barrelContainer).AsSingle();
            
            Container.BindInstance(_checkPointHandler).AsSingle();
            
            BindLevelUpdater();

            Container.BindInstance(_levelFreezer).AsSingle();

            BindLevelStateFactory();
            BindLevelStateMachine();

            BindBarrelFactory();

            BindLevelInitializer();
            
            Container.BindInstance(_pauseWindow).AsSingle();
            Container.BindInstance(_loseWindow).AsSingle();
        }
        
        private void BindInputController()
        {
            Container
                .BindInterfacesTo<InputController>()
                .AsSingle();
        }
        
        private void BindInputHandler()
        {
            if (_isMobileControl || Application.isMobilePlatform)
            {
                Container
                    .BindInterfacesAndSelfTo<MobileInputHandler>()
                    .AsSingle();
                return;
            }

            Container
                .BindInterfacesAndSelfTo<PCInputHandler>()
                .AsSingle();
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
            var levelUpdatables = 
                new ILevelUpdatable[] { _moon, _boat, _water, _cameraMovement }.Concat(_tentacles);
            
            Container
                .BindInterfacesTo<LevelUpdater>()
                .AsSingle()
                .WithArguments(levelUpdatables);
        }

        private void BindLevelInitializer()
        {
            Container
                .BindInterfacesTo<LevelInitializer>()
                .AsSingle();
        }
    }
}