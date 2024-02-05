using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : MonoBehaviour, IStateMachine<GameStateBase>
{
    private readonly List<GameStateBase> _states = new List<GameStateBase>();
    private GameStateBase _current;

    public GameStateBase CurrentState => _current;

    public void Construct()
    {
        _states.AddRange(new GameStateBase[]
        {
            new MenuGameState(this),
            new LevelGameState(this)
        });
    }

    public void ChangeState<T>() where T : GameStateBase
    {
        _current?.Exit();

        _current = _states.Find(x => x is T);
        _current?.Enter();
    }
}