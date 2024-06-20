using System;
using System.Linq;
using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Core.Input;
using Muchachos.TideTurner.Runtime.Level.FloatingObjects;
using Muchachos.TideTurner.Runtime.Level.Obstacles;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Cannon;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Shark;
using Muchachos.TideTurner.Runtime.Level.Savings;
using Muchachos.TideTurner.Runtime.Mobile;
using Muchachos.TideTurner.Runtime.UI;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class LevelBootstrap : Bootstrap
    {
        private BallPool _ballPool;
        private SharkContainer _sharkContainer;
        private BarrelContainer _barrelContainer;
        private ILevelUpdater _updater;

        [Inject]
        public void Construct(ILevelUpdater updater)
        {
            _updater = updater;
        }

        public override void Construct()
        {
            var input = FindAnyObjectByType<InputController>();
            var level = FindAnyObjectByType<Level>();
        
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
            var tentacles = FindObjectsByType<Tentacle>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            var checks = FindObjectsByType<CheckPoint>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            var checkHandler = FindAnyObjectByType<CheckPointHandler>();
            var mobileScreenOrientation = FindAnyObjectByType<MobileScreenOrientation>();

            freezer.Construct(restarter);

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

            level.Construct(moonData, moon, boat, water, cannons, cameraMovement, checkHandler);
        
            pause.Construct();
            _ballPool.Construct(level);
            _sharkContainer.Construct(sharkSpawns, water, level);
            _barrelContainer.Construct(barrelSpawns, moonData, water.Movement, level);
            checkHandler.Construct(checks, level);
        
            foreach (Obstacle obstacle in obstacles)
                obstacle.Construct(level);

            foreach (Cannon cannon in cannons)
                cannon.Construct(_ballPool);

            foreach (WaterBeing being in beings)
                being.Construct(water);

            foreach (FloatingObject floating in floatings)
                floating.Construct(water.Movement);

            foreach (Tentacle tentacle in tentacles)
                tentacle.Construct(water);

            _updater.AddRange(new ILevelUpdatable[] { moon, boat, water, cameraMovement }.Concat(beings).Concat(floatings).Concat(tentacles));
        }

        public override void Init()
        {
            _ballPool.Init();
            _sharkContainer.Init();
            _barrelContainer.Init();
        }
    }
}