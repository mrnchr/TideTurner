using UnityEngine;

public class StopLevelState : LevelStateBase
{
    private readonly LevelUpdater _updater;
    private readonly InputController _input;
    private readonly SoundRestarter _sound;

    public StopLevelState(LevelStateMachine machine) : base(machine)
    {
        _updater = Object.FindAnyObjectByType<LevelUpdater>();
        _input = Object.FindAnyObjectByType<InputController>();
        _sound = Object.FindAnyObjectByType<SoundRestarter>();
    }

    public override void Enter()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        _input.SetPause(true);
        _updater.SetPause(true);
        _sound.FindAll();
        _sound.SoundAll(SoundState.Pause);
    }

    public override void Exit()
    {
        Time.timeScale = 1;
        _input.SetPause(false);
        _updater.SetPause(false);
        _sound.SoundAll(SoundState.Play);
    }
}