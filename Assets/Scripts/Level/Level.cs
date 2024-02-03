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

    public void Construct()
    {
        _machine = FindAnyObjectByType<LevelStateMachine>();
        _moon = FindAnyObjectByType<Moon>();
        _boat = FindAnyObjectByType<Boat>();
        _pause = FindAnyObjectByType<PauseWindow>();

        _moon.Construct();
        _boat.Construct();
        _machine.Construct();
        _pause.Construct();

        _updatables.AddRange(new ILevelUpdatable[]
        {
            _moon,
            _boat
        });
    }

    public void Init()
    {
        _machine.ChangeState<StartLevelState>();
    }

    public void SetPause(bool value)
    {
        _isPaused = value;
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