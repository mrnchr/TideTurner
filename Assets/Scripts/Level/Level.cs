using DefaultNamespace.Level;
using UnityEngine;

public class Level : MonoBehaviour
{
    private LevelStateMachine _machine;
    private Moon _moon;
    private Boat _boat;

    public void Construct()
    {
        _machine = FindAnyObjectByType<LevelStateMachine>();
        _moon = FindAnyObjectByType<Moon>();
        _boat = FindAnyObjectByType<Boat>();

        _moon.Construct();
        _boat.Construct();
        _machine.Construct();
    }

    public void Init()
    {
        _machine.ChangeState<StartLevelState>();
    }
}