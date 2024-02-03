using DefaultNamespace.Level;
using UnityEngine;

public class StartLevelState : LevelStateBase
{
    private readonly Level _level;

    public StartLevelState(LevelStateMachine machine) : base(machine)
    {
        _level = Object.FindAnyObjectByType<Level>();
    }

    public override void Enter()
    {
        _level.Init();
        _machine.ChangeState<StayLevelState>();
    }

    public override void Exit()
    {
    }
}