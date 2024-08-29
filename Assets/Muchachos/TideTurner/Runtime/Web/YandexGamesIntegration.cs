using System;
using System.Runtime.InteropServices;
using UnityEngine;
using Zenject;

public class YandexGamesIntegration : IInitializable, IDisposable
{
    [DllImport("__Internal")]
    private static extern void RateGame();

    [DllImport("__Internal")]
    private static extern void ShowAdv();


    [DllImport("__Internal")]
    private static extern void SendDataToServer(string data);
    [DllImport("__Internal")]
    private static extern void LoadData();

    private readonly UserData _userData;

    [Inject]
    public YandexGamesIntegration(UserData userData)
    {
        _userData = userData;

        _userData.OnDataUpdate += SaveData;
    }
    
    [Inject]
    public void Initialize()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        LoadData();
#endif
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
    }

    public void SetData(string data)
    {
        int ind = JsonUtility.FromJson<UserData>(data).CurrentInd;
        _userData.UpdateCheckPointIndex(ind);
    }

    public void Dispose()
    {
        _userData.OnDataUpdate -= SaveData;
    }
}