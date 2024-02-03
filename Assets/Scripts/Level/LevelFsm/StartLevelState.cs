using DefaultNamespace.Level;
using UnityEngine;

public class StartLevelState : LevelStateBase
{
    private readonly Moon _moon;
    private readonly Water _water;

    public StartLevelState(LevelStateMachine machine) : base(machine)
    {
        _moon = Object.FindAnyObjectByType<Moon>();
        _water = Object.FindAnyObjectByType<Water>();
    }

    public override void Enter()
    {
        _moon.Init();
        _water.Init();
        _machine.ChangeState<StayLevelState>();
    }

    public override void Exit()
    {
    }
}