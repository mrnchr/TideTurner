using System;
using System.ComponentModel;
using UnityEngine;

[Serializable]
public class UserData
{
    public event Action OnDataUpdate;

    public int CurrentInd { get; private set; } = -1;

    public void UpdateCheckPointIndex(int ind)
    {
        if (ind <= CurrentInd)
        {
            Debug.Log($"Index {ind} <= CurrentIndex {CurrentInd}");
            return;
        }
        
        CurrentInd = ind;
        
        Debug.Log("Data updated");
        
        OnDataUpdate?.Invoke();
    }
}