using DefaultNamespace.UI;
using UnityEngine;

public class WinLevelState : LevelStateBase
{
    private readonly LevelFreezer _freezer;
    private readonly WinWindow _win;

    public WinLevelState(LevelStateMachine machine) : base(machine)
    {
        _freezer = Object.FindAnyObjectByType<LevelFreezer>();
        _win = Object.FindAnyObjectByType<WinWindow>();
    }

    public override void Enter()
    {
        _freezer.Freeze();
        _win.SetActive(true);
    }

    public override void Exit()
    {
        _freezer.Unfreeze();
        _win.SetActive(false);
    }
}