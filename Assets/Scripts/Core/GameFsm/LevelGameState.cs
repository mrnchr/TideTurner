using UnityEngine;

public class LevelGameState : GameStateBase
{
    public LevelGameState(GameStateMachine machine) : base(machine)
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