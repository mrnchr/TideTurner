using DefaultNamespace.UI;
using System.Collections;
using DefaultNamespace.Core;
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
    private SceneLoader _sceneLoader;
    private CheckPointHandler _handler;

    public void Construct(LevelStateMachine machine,
        MoonData moonData,
        Moon moon,
        Boat boat,
        Water water,
        LoseWindow lose,
        WinWindow win,
        Cannon[] cannons,
        CameraMovement cameraMovement,
        SceneLoader sceneLoader,
        CheckPointHandler handler)
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
        _sceneLoader = sceneLoader;
        _handler = handler;
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

        _handler.Init();
    }

    public void Reborn()
    {
        CheckPoint last = _handler.GetLastCheck();
        
        _moonData.Init();
        _moon.Init();
        _boat.SetPosition(last.transform.position);
        _boat.ResetLogic();
        _water.Movement.SetWaterLevel(last.transform.position);
        _cameraMovement.Init();
        
        foreach (Cannon cannon in _cannons)
            cannon.Init();
    }

    public void CallReborn()
    {
        if (!_handler.GetLastCheck())
            Restart();
        else
            _machine.ChangeState<RebornLevelState>();
    }

    public void ToMenu()
    {
        _machine.ChangeState<StayLevelState>();
        _sceneLoader.LoadScene(0);
    }

    public void Restart()
    {
        _machine.ChangeState<RestartLevelState>();
    }

    public void Lose()
    {
        if (IsLose() || _machine.CurrentState is WinLevelState)
            return;

        _boat.SetLoseState();
        _coroutine = StartCoroutine(StartDeathTimer());
    }

    public bool IsLose() => _machine.CurrentState is LoseLevelState || _coroutine != null;

    private IEnumerator StartDeathTimer()
    {
        yield return new WaitForSeconds(deathDelay);

        _machine.ChangeState<LoseLevelState>();

        _coroutine = null;
    }

    public void Win()
    {
        if (_machine.CurrentState is WinLevelState || IsLose())
            return;

        _machine.ChangeState<WinLevelState>();
    }
}