using UnityEngine;

public class MenuGameState : GameStateBase
{
    public MenuGameState(GameStateMachine machine) : base(machine)
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