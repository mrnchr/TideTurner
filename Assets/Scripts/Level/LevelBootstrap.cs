using System.Linq;
using DefaultNamespace;
using DefaultNamespace.UI;
using UnityEngine;

public class LevelBootstrap : MonoBehaviour
{
    private BallPool _ballPool;
    private LevelStateMachine _machine;
    private SharkContainer _sharkContainer;

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
            
        moonData.Construct();
        moon.Construct(moonData);
        boat.Construct(moonData, boatSpawn);
        // _barrel.Construct();
        water.Construct(moonData);
        _machine.Construct();
        level.Construct(_machine, moonData, moon, boat, water, lose, win, cannons);
        pause.Construct();
        _ballPool.Construct(level);
        _sharkContainer.Construct(sharkSpawns, water, updater, level);

        foreach (Obstacle obstacle in obstacles)
            obstacle.Construct(level);

        foreach (Cannon cannon in cannons)
            cannon.Construct(_ballPool);

        foreach (WaterBeing being in beings)
            being.Construct(water);

        updater.AddRange(new ILevelUpdatable[]{ moon, boat, water}.Concat(beings));
    }

    public void Init()
    {
        _ballPool.Init();
        _sharkContainer.Init();
        _machine.ChangeState<StartLevelState>();
    }
}