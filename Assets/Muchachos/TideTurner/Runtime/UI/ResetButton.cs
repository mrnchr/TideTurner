using UnityEngine;
using Zenject;

public class ResetButton : MonoBehaviour
{
    private User _user;
    
    [Inject]
    public void Construct(User user)
    {
        _user = user;
    }
    
    public void ResetData()
    {
        _user.Reset();
    }
}
