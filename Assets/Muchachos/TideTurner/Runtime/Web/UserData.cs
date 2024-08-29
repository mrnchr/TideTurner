using System;

[Serializable]
public class UserData
{
    public event Action OnDataUpdate;

    public int CurrentInd { get; private set; } = -1;

    public void UpdateCheckPointIndex(int ind)
    {
        if (ind < 0)
            throw new IndexOutOfRangeException();
        
        CurrentInd = ind;
        
        OnDataUpdate?.Invoke();
    }
}