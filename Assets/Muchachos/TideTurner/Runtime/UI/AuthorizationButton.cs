using UnityEngine;
using Zenject;

public class AuthorizationButton : MonoBehaviour
{
    private YandexGamesIntegration _integration;
    
    [Inject]
    public void Construct(YandexGamesIntegration integration, User user)
    {
        _integration = integration;

        if (user.Data.Authorised)
        {
            Disable();
            return;
        }

        _integration.OnAuth += Disable;
    }
    
    public void Authorize()
    {
        _integration.Authorize();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        _integration.OnAuth -= Disable;
    }
}
