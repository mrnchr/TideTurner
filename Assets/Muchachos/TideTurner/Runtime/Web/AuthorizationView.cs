using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;

public class AuthorizationView : MonoBehaviour
{
    [SerializeField] private LocalizedString localizedString;
    [SerializeField] private TextMeshProUGUI text ;

    private User _user;
    private Action<string> _actionCached;

    [Inject]
    public void Construct(User user)
    {
        _user = user;
        
        _actionCached =  _ =>
        {
            GreetingPlayer();
            UpdateGreeting();
        };

        user.OnNickUpdate += _actionCached.Invoke;
        
        localizedString.Arguments = new object[] { _user.Data.Nick };
        localizedString.StringChanged += UpdateText;
        
        if (_user.Data.Authorised)
        {
            _actionCached?.Invoke(_user.Data.Nick);
        }
    }

    private void GreetingPlayer()
    {
        text.gameObject.SetActive(true);
    }

    private void UpdateText(string newText)
    {
        text.text = newText;
    }
    
    private void UpdateGreeting()
    {
        localizedString.Arguments[0] =  _user.Data.Nick;
        localizedString.RefreshString();
    }

    private void OnDestroy()
    {
        _user.OnNickUpdate -= _actionCached.Invoke;
        localizedString.StringChanged -= UpdateText;
    }
}