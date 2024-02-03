using UnityEngine;

public class StopLevelState : LevelStateBase
{
    private readonly LevelUpdater _updater;
    private readonly InputController _input;

    public StopLevelState(LevelStateMachine machine) : base(machine)
    {
        _updater = Object.FindAnyObjectByType<LevelUpdater>();
        _input = Object.FindAnyObjectByType<InputController>();
    }

    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        _input.SetPause(true);
        _updater.SetPause(true);
    }

    public override void Exit()
    {
        Time.timeScale = 1;
        _input.SetPause(false);
        _updater.SetPause(false);
    }
}