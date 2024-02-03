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
    private Obstacle[] _corrals;
    private Level _level;
    private WinWindow _win;

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
        _corrals = FindObjectsByType<Obstacle>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            
        _moon.Construct();
        _boat.Construct();
        // _barrel.Construct();
        _water.Construct();
        _machine.Construct();
        _level.Construct(_machine, _moon, _boat, _water, _lose, _win);
        _pause.Construct();

        foreach (Obstacle corral in _corrals)
            corral.Construct(_level);
    }

    public void Init()
    {
        _machine.ChangeState<StartLevelState>();
    }
}