using DefaultNamespace.UI;
using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private float deathDelay = 1f;

    private LevelStateMachine _machine;
    private MoonData _moonData;
    private Moon _moon;
    private Boat _boat;
    private Water _water;
    private LoseWindow _lose;
    private WinWindow _win;
    private Cannon[] _cannons;
    private CameraMovement _cameraMovement;
    private Coroutine _coroutine;
    public void Construct(LevelStateMachine machine,
        MoonData moonData,
        Moon moon,
        Boat boat,
        Water water,
        LoseWindow lose,
        WinWindow win,
        Cannon[] cannons,
        CameraMovement cameraMovement)
    {
        _machine = machine;
        _moonData = moonData;
        _moon = moon;
        _boat = boat;
        _water = water;
        _lose = lose;
        _win = win;
        _cannons = cannons;
        _cameraMovement = cameraMovement;
    }

    public void Init()
    {
        _moonData.Init();
        _moon.Init();
        _water.Init();
        _boat.Init();
        _cameraMovement.Init();
        
        foreach (Cannon cannon in _cannons)
            cannon.Init();
    }

    public void Restart()
    {
        _machine.ChangeState<RestartLevelState>();
    }

    public void Lose()
    {
        
        if (_machine.CurrentState is StopLevelState || _coroutine != null)
            return;

        _boat.SetLoseState();
        _coroutine = StartCoroutine(StartDeathTimer());        
    }

    private IEnumerator StartDeathTimer()
    {
        yield return new WaitForSeconds(deathDelay);

        _machine.ChangeState<StopLevelState>();
        _lose.SetActive(true);

        _coroutine = null;
    }

    public void Win()
    {
        if (_machine.CurrentState is StopLevelState)
            return;
        
        _win.SetActive(true);
        _machine.ChangeState<StopLevelState>();
    }
}