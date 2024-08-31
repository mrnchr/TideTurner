using System;
using UnityEngine;

public class User
{
    public event Action OnDataUpdate;

    public UserData Data { get; private set; } = new UserData(-1);

    public void UpdateData(UserData data)
    {
        if (data.CurrentInd <= Data.CurrentInd)
        {
            Debug.Log($"Index ({data.CurrentInd}) <= CurrentIndex({Data.CurrentInd})");
            return;
        }
        
        Data = data;
        
        Debug.Log("Data updated");
        
        OnDataUpdate?.Invoke();
    }
}

[Serializable]
public struct UserData
{
    public int CurrentInd;

    public UserData(int ind)
    {
        CurrentInd = ind;
    }
}