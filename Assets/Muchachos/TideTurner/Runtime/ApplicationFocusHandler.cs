using System;
using UnityEngine;

public class ApplicationFocusHandler : MonoBehaviour
{
    public event Action<bool> OnFocusChange;
    
    public bool IsFocused { get; private set; } = true;
    
    private void OnApplicationFocus(bool hasFocus)
    {
        IsFocused = hasFocus;
        
        OnFocusChange?.Invoke(IsFocused);
    }
}
