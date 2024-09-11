using System;
using UnityEngine;

public class User
{
    public event Action OnDataUpdate;
    public event Action<string> OnNickUpdate;

    public UserData Data { get; private set; } = new UserData("null", -1);

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

    public void UpdateNick(string nick)
    {
        Data.UpdateNick(nick);
        
        OnNickUpdate?.Invoke(nick);
    }

    public void Reset()
    {
        Data = new UserData();
    }
}

[Serializable]
public struct UserData
{
    public int CurrentInd;
    public string Nick;

    public UserData(string nick, int ind)
    {
        CurrentInd = ind;
        Nick = nick;
    }

    public void UpdateNick(string nick) => Nick = nick;
}