using Muchachos.TideTurner.Runtime.Core.Input;
using Muchachos.TideTurner.Runtime.Level.LevelFsm;
using Muchachos.TideTurner.Runtime.Level.Savings;
using UnityEngine;
using Zenject;

public class GamePlayEntryPoint : MonoBehaviour
{
    private CheckPointHandler _checkPointHandler;
    private User _user;

    private YandexGamesIntegration _yandexGamesIntegration;
    private LevelStateMachine _levelStateMachine;
    private int _currentInd;

    [Inject]
    public void Construct(
        CheckPointHandler checkPointHandler,
        User user,
        IInputController inputController,
        YandexGamesIntegration yandexGamesIntegration,
        LevelStateMachine levelStateMachine
    )
    {
        _checkPointHandler = checkPointHandler;
        _user = user;
        _yandexGamesIntegration = yandexGamesIntegration;
        _levelStateMachine = levelStateMachine;

        checkPointHandler.OnNewCheckPoint += WrappedUpdateData;
        user.OnDataUpdate += checkPointHandler.UpdateCheckPointIndex;
        _levelStateMachine.OnChangeState += _yandexGamesIntegration.HandleGamePlayAPI;
    }

    private void WrappedUpdateData(int ind) => _user.UpdateData(new UserData(ind)); 
    
    private void OnDisable()
    {
        _checkPointHandler.OnNewCheckPoint -= WrappedUpdateData;
        _user.OnDataUpdate -= _checkPointHandler.UpdateCheckPointIndex;
    }
}