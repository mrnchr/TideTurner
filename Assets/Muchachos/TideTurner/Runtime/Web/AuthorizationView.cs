using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AuthorizationView : MonoBehaviour
{
    [SerializeField] private Text loggedText;
    [SerializeField] private TextMeshPro text;

    private User _user;

    [Inject]
    public void Construct(User user)
    {
        _user = user;

        _user.OnNickUpdate += GreetingPlayer;
    }

    private void GreetingPlayer(string nick)
    {
        loggedText.enabled = true;
    }

    private void OnDestroy()
    {
        _user.OnNickUpdate -= GreetingPlayer;
    }
}