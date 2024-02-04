using UnityEngine;

public class RestartLevelState : LevelStateBase
{
    private readonly BallPool _ballPool;
    private readonly Cannon[] _cannons;
    private readonly SharkContainer _sharkContainer;
    private readonly SoundRestarter _sound;

    public RestartLevelState(LevelStateMachine machine) : base(machine)
    {   
        _ballPool = Object.FindAnyObjectByType<BallPool>();
        _sharkContainer = Object.FindAnyObjectByType<SharkContainer>();
        _cannons = Object.FindObjectsByType<Cannon>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        _sound = Object.FindAnyObjectByType<SoundRestarter>();
    }

    public override void Enter()
    {
        _sound.SoundAll(SoundState.Stop);
        _ballPool.Clear();
        _sharkContainer.Respawn();
        foreach (Cannon cannon in _cannons)
            cannon.Stop();
            
        _machine.ChangeState<StartLevelState>();
    }

    public override void Exit()
    {
    }
}