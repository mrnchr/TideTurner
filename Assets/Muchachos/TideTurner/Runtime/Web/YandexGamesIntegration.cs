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

    private User _user;
    private ApplicationFocusHandler _applicationFocusHandler;

    [Inject]
    public void Construct(User user, ApplicationFocusHandler applicationFocusHandler)
    {
        _user = user;
        _applicationFocusHandler = applicationFocusHandler;

        _applicationFocusHandler.OnFocusChange += CheckFocus;
        _user.OnDataUpdate += Save;
    }

    public void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        LoadData();
        StopGameplay();
#endif
        Debug.Log("Data loaded");
    }

    public void CallRateGameWindow()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        RateGame();
#endif
    }

    public void CallAdvWindow()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        ShowAdv();
#endif
    }

    private void Save()
    {
        string data = JsonUtility.ToJson(_user.Data);

#if UNITY_WEBGL && !UNITY_EDITOR
        SendDataToServer(data);
#endif
        Debug.Log("Data saved: " + _user.Data.CurrentInd);
    }

    public void SetData(string data)
    {
        var userData = JsonUtility.FromJson<UserData>(data);
        _user.UpdateData(userData);
        
        Debug.Log("Data loaded: " + userData.CurrentInd);
    }

    public void HandleGamePlayAPI(LevelStateBase gameStateBase)
    {
        switch (gameStateBase)
        {
            case LoseLevelState:
#if UNITY_WEBGL && !UNITY_EDITOR
                StopGameplay();
#endif
                Debug.Log("LoseLevelState");
                break;
            case PauseLevelState:
#if UNITY_WEBGL && !UNITY_EDITOR
                StopGameplay();
#endif
                Debug.Log("PauseLevelState");
                break;
            case RebornLevelState:
                break;
            case RestartLevelState:
                break;
            case StartLevelState:
                break;
            case StayLevelState:
#if UNITY_WEBGL && !UNITY_EDITOR
                StartGameplay();
#endif
                Debug.Log("StayLevelState");
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
        
#if UNITY_WEBGL && !UNITY_EDITOR
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