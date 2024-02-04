using System.Linq;
using DefaultNamespace.UI;
using UnityEngine;

public class LevelBootstrap : MonoBehaviour
{
    private BallPool _ballPool;
    private LevelStateMachine _machine;
    private SharkContainer _sharkContainer;
    private BarrelContainer _barrelContainer;

    public void Construct()
    {
        var updater = FindAnyObjectByType<LevelUpdater>();
        var level = FindAnyObjectByType<Level>();
        _machine = FindAnyObjectByType<LevelStateMachine>();
        var moonData = FindAnyObjectByType<MoonData>();
        var moon = FindAnyObjectByType<Moon>();
        var boatSpawn = FindAnyObjectByType<BoatSpawn>();
        var boat = FindAnyObjectByType<Boat>();
        var water = FindAnyObjectByType<Water>();
        // _barrel = FindAnyObjectByType<Barrel>();
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

        moonData.Construct();
        moon.Construct(moonData);
        // _barrel.Construct();
        water.Construct(moonData);
        boat.Construct(moonData, boatSpawn, water.Movement);
        cameraMovement.Construct(boat);
        _machine.Construct();
        level.Construct(_machine, moonData, moon, boat, water, lose, win, cannons, cameraMovement);
        pause.Construct();
        _ballPool.Construct(level);
        _sharkContainer.Construct(sharkSpawns, water, updater, level);
        _barrelContainer.Construct(barrelSpawns, moonData, updater, water.Movement);

        foreach (Obstacle obstacle in obstacles)
            obstacle.Construct(level);

        foreach (Cannon cannon in cannons)
            cannon.Construct(_ballPool);

        foreach (WaterBeing being in beings)
            being.Construct(water);

        foreach (FloatingObject floating in floatings)
        {
            floating.Construct(water.Movement);
        }

        updater.AddRange(new ILevelUpdatable[] { moon, boat, water, cameraMovement }.Concat(beings).Concat(floatings));
    }

    public void Init()
    {
        _ballPool.Init();
        _sharkContainer.Init();
        _barrelContainer.Init();
        _machine.ChangeState<StartLevelState>();
    }
}