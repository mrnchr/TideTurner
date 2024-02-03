using System.Collections.Generic;
using DefaultNamespace.Level;
using DefaultNamespace.UI;
using UnityEngine;

public class Level : MonoBehaviour, ILevelUpdatable
{
    private readonly List<ILevelUpdatable> _updatables = new List<ILevelUpdatable>();
    private LevelStateMachine _machine;
    private Moon _moon;
    private Boat _boat;
    private PauseWindow _pause;
    private Water _water;
    private LoseWindow _lose;
    private bool _isPaused;
    private WinWindow _win;

    public void Construct(LevelStateMachine machine, Moon moon, Boat boat, Water water, LoseWindow lose, WinWindow win)
    {
        _machine = machine;
        _moon = moon;
        _boat = boat;
        _water = water;
        _lose = lose;
        _win = win;

        _updatables.AddRange(new ILevelUpdatable[]
        {
            _moon,
            _boat,
            _water
        });
    }

    public void Init()
    {
        _moon.Init();
        _water.Init();
        _boat.Init();
    }

    public void SetPause(bool value)
    {
        _isPaused = value;
    }

    public void Restart()
    {
        _machine.ChangeState<StartLevelState>();
    }

    public void Lose()
    {
        if (_machine.CurrentState is StopLevelState)
            return;
        
        _lose.SetActive(true);
        _machine.ChangeState<StopLevelState>();
    }

    public void Win()
    {
        if (_machine.CurrentState is StopLevelState)
            return;
        
        _win.SetActive(true);
        _machine.ChangeState<StopLevelState>();
    }

    private void Update()
    {
        if (!_isPaused)
            UpdateLogic();
    }

    public void UpdateLogic()
    {
        foreach (ILevelUpdatable updatable in _updatables)
            updatable.UpdateLogic();
    }
}