using UnityEngine;

public class RestartLevelState : LevelStateBase
{
    private readonly BallPool _ballPool;
    private readonly Cannon[] _cannons;

    public RestartLevelState(LevelStateMachine machine) : base(machine)
    {   
        _ballPool = Object.FindAnyObjectByType<BallPool>();
        _cannons = Object.FindObjectsByType<Cannon>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

    public override void Enter()
    {
        _ballPool.Clear();
        foreach (Cannon cannon in _cannons)
            cannon.Stop();
            
        _machine.ChangeState<StartLevelState>();
    }

    public override void Exit()
    {
    }
}