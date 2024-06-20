using System;
using System.Linq;
using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Core.Input;
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

namespace Muchachos.TideTurner.Runtime.Level
{
    public class LevelBootstrap : Bootstrap
    {
        private BallPool _ballPool;
        private LevelStateMachine _machine;
        private SharkContainer _sharkContainer;
        private BarrelContainer _barrelContainer;

        public override void Construct()
        {
            var input = FindAnyObjectByType<InputController>();
            var updater = FindAnyObjectByType<LevelUpdater>();
            var level = FindAnyObjectByType<Level>();
            _machine = FindAnyObjectByType<LevelStateMachine>();
        
            AbstractMoon moon = FindFirstObjectByType<AbstractMoon>();
            AbstractMoonData moonData = FindFirstObjectByType<AbstractMoonData>();
        
            var boatSpawn = FindAnyObjectByType<BoatSpawn>();
            var boat = FindAnyObjectByType<Boat>();
            var water = FindAnyObjectByType<Water>();
            var pause = FindAnyObjectByType<PauseWindow>();
            var lose = FindAnyObjectByType<LoseWindow>();
            var win = FindAnyObjectByType<WinWindow>();
            _ballPool = FindAnyObjectByType<BallPool>();
            var obstacles = FindObjectsByType<Obstacle>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            var cannons = FindObjectsByType<Cannon>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            var beings = FindObjectsByType<WaterBeing>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            var sharkSpawns = FindObjectsByType<SharkSpawn>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _sharkContainer = FindAnyObjectByType<SharkContainer>();
            var cameraMovement = FindAnyObjectByType<CameraMovement>();
            var floatings = FindObjectsByType<FloatingObject>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            var barrelSpawns = FindObjectsByType<BarrelSpawn>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            _barrelContainer = FindAnyObjectByType<BarrelContainer>();
            var restarter = FindAnyObjectByType<SoundRestarter>();
            var freezer = FindAnyObjectByType<LevelFreezer>();
            var loader = FindAnyObjectByType<SceneLoader>();
            var buttons = FindObjectsByType<ButtonSoundCaller>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            var tentacles = FindObjectsByType<Tentacle>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            var checks = FindObjectsByType<CheckPoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            var checkHandler = FindAnyObjectByType<CheckPointHandler>();
            var mobileScreenOrientation = FindAnyObjectByType<MobileScreenOrientation>();

            input.Construct();
            freezer.Construct(updater, restarter);

            moonData.Construct();
            switch (moon)
            {
                case Moon m:
                    m.Construct((MoonData)moonData);
                    break;
                case MobileMoon:
                    mobileScreenOrientation.Construct(moonData, input, cameraMovement);
                    break;
                default:
                    throw new Exception("Unknown moon");
            }
        
            water.Construct(moonData);
            boat.Construct(moonData, boatSpawn, water.Movement);
            cameraMovement.Construct(boat);
            _machine.Construct();

            level.Construct(_machine, moonData, moon, boat, water, lose, win, cannons, cameraMovement, loader, checkHandler);
        
            pause.Construct();
            _ballPool.Construct(level);
            _sharkContainer.Construct(sharkSpawns, water, updater, level);
            _barrelContainer.Construct(barrelSpawns, moonData, updater, water.Movement, level);
            checkHandler.Construct(checks, level);
        
            foreach (Obstacle obstacle in obstacles)
                obstacle.Construct(level);

            foreach (Cannon cannon in cannons)
                cannon.Construct(_ballPool);

            foreach (WaterBeing being in beings)
                being.Construct(water);

            foreach (FloatingObject floating in floatings)
                floating.Construct(water.Movement);

            foreach (ButtonSoundCaller button in buttons)
                button.Construct();

            foreach (Tentacle tentacle in tentacles)
                tentacle.Construct(water);

            updater.AddRange(new ILevelUpdatable[] { moon, boat, water, cameraMovement }.Concat(beings).Concat<ILevelUpdatable>(floatings).Concat(tentacles));
        }

        public override void Init()
        {
            _ballPool.Init();
            _sharkContainer.Init();
            _barrelContainer.Init();
            _machine.ChangeState<StartLevelState>();
        }
    }
}