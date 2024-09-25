using System;
using UnityEngine;

public class User
{
    public const int DefaultInd = -1;
    
    public event Action OnDataUpdate;
    public event Action<string> OnNickUpdate;
    public event Action OnReset;

    public UserData Data => _data;
    private UserData _data = new UserData("null", DefaultInd, false);

    public void UpdateData(UserData data)
    {
        if (data.CurrentInd <= Data.CurrentInd)
        {
            Debug.Log($"Index ({data.CurrentInd}) <= CurrentIndex({Data.CurrentInd})");
            return;
        }

        _data = data;

        Debug.Log("Data updated");

        OnDataUpdate?.Invoke();
    }

    public void UpdateNick(string nick)
    {
        _data.Nick = nick;

        _data.Authorised = true;
        
        OnNickUpdate?.Invoke(nick);
    }

    public void Reset()
    {
        _data.CurrentInd = DefaultInd;
        
        OnReset?.Invoke();
        
        Debug.Log("Reset completed");
    }
}

[Serializable]
public struct UserData
{
    public int CurrentInd;
    public string Nick;
    public bool Authorised;

    public UserData(string nick, int ind, bool authorised)
    {
        CurrentInd = ind;
        Nick = nick;
        Authorised = authorised;
    }
}