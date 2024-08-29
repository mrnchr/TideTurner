using Muchachos.TideTurner.Runtime.Level.Savings;
using UnityEngine;
using Zenject;

public class GamePlayEntryPoint : MonoBehaviour
{
    private CheckPointHandler _checkPointHandler;
    private UserData _userData;
    
    [Inject]
    public void Construct(CheckPointHandler checkPointHandler, UserData userData)
    {
        _checkPointHandler = checkPointHandler;
        _userData = userData;
        
        checkPointHandler.OnNewCheckPoint += userData.UpdateCheckPointIndex;
    }

    private void OnDisable()
    {
        _checkPointHandler.OnNewCheckPoint -= _userData.UpdateCheckPointIndex;
    }
}
