using System.Collections.Generic;
using UnityEngine;

public class LevelUpdater : MonoBehaviour
{
    private readonly List<ILevelUpdatable> _updatables = new List<ILevelUpdatable>();
    private bool _isPaused;

    public void Add(ILevelUpdatable updatable)
    {
        _updatables.Add(updatable);
    }

    public void AddRange(IEnumerable<ILevelUpdatable> updatable)
    {
        _updatables.AddRange(updatable);
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