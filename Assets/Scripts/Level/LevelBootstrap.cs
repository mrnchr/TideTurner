using DefaultNamespace.Level;
using DefaultNamespace.UI;
using UnityEngine;

public class LevelBootstrap : MonoBehaviour
{
    private LevelStateMachine _machine;
    private Moon _moon;
    private Boat _boat;
    private Water _water;
    private PauseWindow _pause;
    private LoseWindow _lose;
    private Obstacle[] _obstacles;
    private Level _level;
    private WinWindow _win;
    private BallPool _ballPool;
    private Cannon[] _cannons;

    public void Construct()
    {
        _level = FindAnyObjectByType<Level>();
        _machine = FindAnyObjectByType<LevelStateMachine>();
        _moon = FindAnyObjectByType<Moon>();
        _boat = FindAnyObjectByType<Boat>();
        _water = FindAnyObjectByType<Water>();
        // _barrel = FindAnyObjectByType<Barrel>();
        _pause = FindAnyObjectByType<PauseWindow>();
        _lose = FindAnyObjectByType<LoseWindow>();
        _win = FindAnyObjectByType<WinWindow>();
        _ballPool = FindAnyObjectByType<BallPool>();
        _obstacles = FindObjectsByType<Obstacle>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        _cannons = FindObjectsByType<Cannon>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            
        _moon.Construct();
        _boat.Construct();
        // _barrel.Construct();
        _water.Construct();
        _machine.Construct();
        _level.Construct(_machine, _moon, _boat, _water, _lose, _win, _cannons);
        _pause.Construct();
        _ballPool.Construct();

        foreach (Obstacle obstacle in _obstacles)
            obstacle.Construct(_level);

        foreach (Cannon cannon in _cannons)
            cannon.Construct(_ballPool);
    }

    public void Init()
    {
        _ballPool.Init();
        _machine.ChangeState<StartLevelState>();
    }
}