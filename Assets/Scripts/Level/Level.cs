using UnityEngine;

public class Level : MonoBehaviour
{
    private LevelStateMachine _machine;
    private Moon _moon;

    public void Construct()
    {
        _machine = FindAnyObjectByType<LevelStateMachine>();
        _moon = FindAnyObjectByType<Moon>();
        
        _moon.Construct();
        _machine.Construct();
    }

    public void Init()
    {
        _machine.ChangeState<StartLevelState>();
    }
}