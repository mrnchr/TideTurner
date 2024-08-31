using Muchachos.TideTurner.Runtime.Core.Input;
using Muchachos.TideTurner.Runtime.Level.LevelFsm;
using Muchachos.TideTurner.Runtime.Level.Savings;
using UnityEngine;
using Zenject;

public class GamePlayEntryPoint : MonoBehaviour
{
    private CheckPointHandler _checkPointHandler;
    private UserData _userData;

    private YandexGamesIntegration _yandexGamesIntegration;
    private LevelStateMachine _levelStateMachine;

    [Inject]
    public void Construct(
        CheckPointHandler checkPointHandler,
        UserData userData,
        IInputController inputController, 
        YandexGamesIntegration yandexGamesIntegration,
        LevelStateMachine levelStateMachine
    )
    {
        _checkPointHandler = checkPointHandler;
        _userData = userData;
        _yandexGamesIntegration = yandexGamesIntegration;
        _levelStateMachine = levelStateMachine;

        checkPointHandler.OnNewCheckPoint += userData.UpdateCheckPointIndex;
        userData.OnDataUpdate += checkPointHandler.UpdateCheckPointIndex;
        _levelStateMachine.OnChangeState += _yandexGamesIntegration.HandleGamePlayAPI;
    }

    private void OnDisable()
    {
        _checkPointHandler.OnNewCheckPoint -= _userData.UpdateCheckPointIndex;
        _userData.OnDataUpdate -= _checkPointHandler.UpdateCheckPointIndex;
    }
}