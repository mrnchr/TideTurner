using Muchachos.TideTurner.Runtime.Core.Input;
using UnityEngine;
using UnityEngine.UI;

namespace Muchachos.TideTurner.Runtime.Mobile
{
    public class MobileMenu : MonoBehaviour
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private Button continueButton;
    
        private InputController _input;
    
        private void Awake()
        {
            _input = FindAnyObjectByType<InputController>();
        
            menuButton.onClick.AddListener(_input.ChangePauseState);
            continueButton.onClick.AddListener(_input.ChangePauseState);
        }
    }
}
