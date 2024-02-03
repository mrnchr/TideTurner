using UnityEngine;

public class StartLevelState : LevelStateBase
{
    private readonly Moon _moon;

    public StartLevelState(LevelStateMachine machine) : base(machine)
    {
        _moon = Object.FindAnyObjectByType<Moon>();
    }

    public override void Enter()
    {
        _moon.Init();
        _machine.ChangeState<StayLevelState>();
    }

    public override void Exit()
    {
    }
}