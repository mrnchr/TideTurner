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

    private UserData _userData;
    private ApplicationFocusHandler _applicationFocusHandler;

    [Inject]
    public void Construct(UserData userData, ApplicationFocusHandler applicationFocusHandler)
    {
        _userData = userData;
        _applicationFocusHandler = applicationFocusHandler;

        _applicationFocusHandler.OnFocusChange += CheckFocus;
        _userData.OnDataUpdate += SaveData;
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

    private void SaveData()
    {
        string data = JsonUtility.ToJson(_userData);

#if UNITY_WEBGL && !UNITY_EDITOR
        SendDataToServer(data);
#endif
        Debug.Log("Data saved: " + _userData.CurrentInd);
    }

    public void SetData(string data)
    {
        int ind = JsonUtility.FromJson<UserData>(data).CurrentInd;
        _userData.UpdateCheckPointIndex(ind);
        
        Debug.Log("Data loaded: " + ind);
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
        _userData.OnDataUpdate -= SaveData;
        _applicationFocusHandler.OnFocusChange -= CheckFocus;
        
        SaveData();
    }
}