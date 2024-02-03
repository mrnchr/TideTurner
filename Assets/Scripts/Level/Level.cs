using System.Collections.Generic;
using DefaultNamespace.Level;
using DefaultNamespace.UI;
using UnityEngine;

public class Level : MonoBehaviour, ILevelUpdatable
{
    private LevelStateMachine _machine;
    private Moon _moon;
    private Boat _boat;
    private readonly List<ILevelUpdatable> _updatables = new List<ILevelUpdatable>();
    private bool _isPaused;
    private PauseWindow _pause;
    private Water _water;
    private Barrel _barrel;
    private LoseWindow _lose;
    private Corral[] _corrals;

    public void Construct()
    {
        _machine = FindAnyObjectByType<LevelStateMachine>();
        _moon = FindAnyObjectByType<Moon>();
        _boat = FindAnyObjectByType<Boat>();
        _water = FindAnyObjectByType<Water>();
        _barrel = FindAnyObjectByType<Barrel>();
        _pause = FindAnyObjectByType<PauseWindow>();
        _lose = FindAnyObjectByType<LoseWindow>();
        _corrals = FindObjectsByType<Corral>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        _moon.Construct();
        _boat.Construct();
        _barrel.Construct();
        _water.Construct();
        _machine.Construct();
        _pause.Construct();

        foreach (Corral corral in _corrals)
        {
            corral.Construct(this);
        }

        _updatables.AddRange(new ILevelUpdatable[]
        {
            _moon,
            _boat,
            _water
        });
    }

    public void Init()
    {
        Debug.Log("Init");
        _machine.ChangeState<StartLevelState>();
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

    private void Update()
    {
        Debug.Log(_isPaused);
        if (!_isPaused)
            UpdateLogic();
    }

    public void UpdateLogic()
    {
        foreach (ILevelUpdatable updatable in _updatables)
            updatable.UpdateLogic();
    }
}