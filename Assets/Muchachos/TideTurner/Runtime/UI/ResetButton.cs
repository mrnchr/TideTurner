using UnityEngine;
using Zenject;

public class ResetButton : MonoBehaviour
{
    private YandexGamesIntegration _yandexGamesIntegration;
    
    [Inject]
    public void Construct(YandexGamesIntegration yandexGamesIntegration)
    {
        _yandexGamesIntegration = yandexGamesIntegration;
    }
    
    public void ResetData()
    {
        _yandexGamesIntegration.ResetData();
    }
}
