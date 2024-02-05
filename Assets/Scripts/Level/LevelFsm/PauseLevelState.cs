using UnityEngine;

public class PauseLevelState : LevelStateBase
{
    private readonly LevelFreezer _freezer;
    private readonly PauseWindow _pause;

    public PauseLevelState(LevelStateMachine machine) : base(machine)
    {
        _freezer = Object.FindAnyObjectByType<LevelFreezer>();
    }

    public override void Enter()
    {
        _freezer.Freeze();
    }

    public override void Exit()
    {
        _freezer.Unfreeze();
    }
}