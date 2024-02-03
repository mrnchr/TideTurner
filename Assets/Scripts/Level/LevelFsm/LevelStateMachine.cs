using System.Collections.Generic;
using UnityEngine;

public class LevelStateMachine : MonoBehaviour, IStateMachine<LevelStateBase>
{
    private readonly List<LevelStateBase> _states = new List<LevelStateBase>();
    private LevelStateBase _current;

    public LevelStateBase CurrentState => _current;

    public void Construct()
    {
        _states.AddRange(new LevelStateBase[]
        {
            new StartLevelState(this),
            new StayLevelState(this),
            new StopLevelState(this)
        });
    }
        
    public void ChangeState<T>() where T : LevelStateBase
    {
        _current?.Exit();

        _current = _states.Find(x => x is T);
        _current?.Enter();
    }
}