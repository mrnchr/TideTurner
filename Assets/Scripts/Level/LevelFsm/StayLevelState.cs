using UnityEngine;

public class StayLevelState : LevelStateBase
{
    public StayLevelState(LevelStateMachine machine) : base(machine)
    {
    }

    public override void Enter()
    {
        if (Application.isMobilePlatform == false)
            Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Exit()
    {
    }
}