using UnityEngine;

public class LevelGameState : GameStateBase
{
    public LevelGameState(GameStateMachine machine) : base(machine)
    {
    }

    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public override void Exit()
    {
    }
}