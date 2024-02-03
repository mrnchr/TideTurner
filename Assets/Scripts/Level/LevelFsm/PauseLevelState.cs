using UnityEngine;

public class PauseLevelState : LevelStateBase
{
    public PauseLevelState(LevelStateMachine machine) : base(machine)
    {
    }

    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public override void Exit()
    {
    }
}