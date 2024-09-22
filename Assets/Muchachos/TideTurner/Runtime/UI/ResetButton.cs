using UnityEngine;
using Zenject;

public class ResetButton : MonoBehaviour
{
    private YandexGamesIntegration _yandexGamesIntegration;
    private User _user;
    
    [Inject]
    public void Construct(YandexGamesIntegration yandexGamesIntegration, User user)
    {
        _yandexGamesIntegration = yandexGamesIntegration;
        _user = user;
    }
    
    public void ResetData()
    {
        if (!_user.Data.Authorised)
            return;
        
        _yandexGamesIntegration.ResetData();
    }
}
