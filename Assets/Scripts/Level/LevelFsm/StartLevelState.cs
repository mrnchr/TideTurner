using DefaultNamespace.Level;
using UnityEngine;

public class StartLevelState : LevelStateBase
{
    private readonly Moon _moon;
    private readonly Water _water;
    private readonly Boat _boat;

    public StartLevelState(LevelStateMachine machine) : base(machine)
    {
        _moon = Object.FindAnyObjectByType<Moon>();
        _water = Object.FindAnyObjectByType<Water>();
        _boat = Object.FindAnyObjectByType<Boat>();
    }

    public override void Enter()
    {
        _moon.Init();
        _water.Init();
        _boat.Init();
        
        _machine.ChangeState<StayLevelState>();
    }

    public override void Exit()
    {
    }
}