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
    private static extern void LoadApiReady();
    
    [DllImport("__Internal")]
    private static extern void Language();

    public event Action OnAuth;
    public event Action OnAdv;
    public event Action OnAdvEnd;

    private User _user;
    private ApplicationFocusHandler _applicationFocusHandler;
    private LocalizationController _localizationController;
    private string _authFlag = "false";

    [Inject]
    public void Construct(User user,
        ApplicationFocusHandler applicationFocusHandler,
        LocalizationController localizationController)
    {
        _user = user;
        _applicationFocusHandler = applicationFocusHandler;
        _localizationController = localizationController;

        _applicationFocusHandler.OnFocusChange += CheckFocus;
        _user.OnDataUpdate += Save;
        _user.OnReset += Save;
#if !UNITY_EDITOR
        OnAuth += LoadData;
#endif
    }

    public void Start()
    {
#if !UNITY_EDITOR
        LoadApiReady();

        Debug.Log("Initializing lang");
        Language();
#endif
        Debug.Log("Initialize completed");
    }
    
    public void Authorize()
    {
        Debug.Log("Authorizing");
        
#if !UNITY_EDITOR
        Auth();
#endif
#if UNITY_EDITOR
        CheckAuth("true");
        SetNick("USERNAME");
#endif
    }

    public void CallRateGameWindow()
    {
#if !UNITY_EDITOR
        RateGame();
#endif
    }

    public void CallAdvWindow()
    {
        OnAdv?.Invoke();
#if !UNITY_EDITOR
        ShowAdv();
#endif
    }

    private void Save()
    {
        if (_authFlag == "false")
            return;
        
        string data = JsonUtility.ToJson(_user.Data);

#if !UNITY_EDITOR
        SendDataToServer(data);
#endif
        Debug.Log("Data saved: " + _user.Data.CurrentInd);
    }
    
    // call from js
    public void CheckAuth(string state)
    {
        _authFlag = state;

        if (_authFlag == "true")
        {
            OnAuth?.Invoke();
            Debug.Log("Auth completed");
        }

        Debug.Log("Auth state: " + _authFlag);
    }

    // call from js
    public void SetData(string data)
    {
        var userData = JsonUtility.FromJson<UserData>(data);
        
        Debug.Log("Data loaded: " + userData.CurrentInd);
        
        _user.UpdateData(userData);
    }

    // call from js
    public void SetNick(string nick)
    {
        _user.UpdateNick(nick);
    }
    
    // call from js
    public void SetLanguage(string lang)
    {
        Debug.Log("Current lang: " + lang);
        
        _localizationController.SetLang(lang);
    }
    
    // call from js
    public void AdvFinished()
    {
        OnAdvEnd?.Invoke();
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
        _user.OnReset -= Save;
        _applicationFocusHandler.OnFocusChange -= CheckFocus;
        OnAuth -= LoadData;

        Save();
    }
}