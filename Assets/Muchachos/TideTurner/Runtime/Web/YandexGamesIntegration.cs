using System;
using System.Runtime.InteropServices;
using Muchachos.TideTurner.Runtime.Level.LevelFsm;
using UnityEngine;
using Zenject;

public class YandexGamesIntegration : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void ShowAdv();


    [DllImport("__Internal")]
    private static extern void SendDataToServer(string data);

    [DllImport("__Internal")]
    private static extern void LoadData();


    [DllImport("__Internal")]
    private static extern void StartGameplay();

    [DllImport("__Internal")]
    private static extern void StopGameplay();


    [DllImport("__Internal")]
    private static extern void Auth();


    [DllImport("__Internal")]
    private static extern void CheckLoginState();

    public event Action OnAuth;

    private User _user;
    private ApplicationFocusHandler _applicationFocusHandler;
    private string _authFlag;

    [Inject]
    public void Construct(User user,
        ApplicationFocusHandler applicationFocusHandler)
    {
        _user = user;
        _applicationFocusHandler = applicationFocusHandler;

        _applicationFocusHandler.OnFocusChange += CheckFocus;
        _user.OnDataUpdate += Save;
    }

    public void Authorize()
    {
        Debug.Log("Authorizing");

#if UNITY_2023
        SetNick("GRAS");
#endif
        
#if !UNITY_EDITOR
        Auth();
        CheckLoginState();
        if (_authFlag == "false")
        {
            Debug.Log("Player is not authorized");
            return;
        }
        LoadData();
        Debug.Log("Data loaded");
#endif
        OnAuth?.Invoke();
    }

    public void Start()
    {
#if !UNITY_EDITOR
        StopGameplay();
#endif
    }

    public void ResetData()
    {
#if !UNITY_EDITOR
        _user.Reset();
        Save();
#endif
        Debug.Log("Reset");
    }

    public void CallRateGameWindow()
    {
#if !UNITY_EDITOR
        RateGame();
#endif
    }

    public void CallAdvWindow()
    {
#if !UNITY_EDITOR
        ShowAdv();
#endif
    }

    private void Save()
    {
        string data = JsonUtility.ToJson(_user.Data);

#if !UNITY_EDITOR
        SendDataToServer(data);
#endif
        Debug.Log("Data saved: " + _user.Data.CurrentInd);
    }

    // call from js
    public void SetData(string data)
    {
        var userData = JsonUtility.FromJson<UserData>(data);
        _user.UpdateData(userData);

        Debug.Log("Data loaded: " + userData.CurrentInd);
    }

    // call from js
    public void SetNick(string nick)
    {
        _user.UpdateNick(nick);
    }

    public void CheckAuth(string state)
    {
        _authFlag = state;

        Debug.Log("Auth state: " + state);
    }

    public void HandleGamePlayAPI(LevelStateBase gameStateBase)
    {
        switch (gameStateBase)
        {
            case LoseLevelState:
#if !UNITY_EDITOR
                StopGameplay();
#endif
                //Debug.Log("LoseLevelState");
                break;
            case PauseLevelState:
#if !UNITY_EDITOR
                StopGameplay();
#endif
                //Debug.Log("PauseLevelState");
                break;
            case RebornLevelState:
                break;
            case RestartLevelState:
                break;
            case StartLevelState:
                break;
            case StayLevelState:
#if !UNITY_EDITOR
                StartGameplay();
#endif
                //Debug.Log("StayLevelState");
                break;
            case WinLevelState:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(gameStateBase));
        }
    }

    private void CheckFocus(bool hasFocus)
    {
        if (hasFocus)
            return;

#if !UNITY_EDITOR
        StopGameplay();
#endif
    }

    public void OnDisable()
    {
        _user.OnDataUpdate -= Save;
        _applicationFocusHandler.OnFocusChange -= CheckFocus;

        Save();
    }
}