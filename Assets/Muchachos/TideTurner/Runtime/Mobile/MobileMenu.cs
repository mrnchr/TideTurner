using Muchachos.TideTurner.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Muchachos.TideTurner.Runtime.Mobile
{
    public class MobileMenu : MonoBehaviour
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private Button continueButton;
    
        private PauseWindow _pauseWindow;

        private void Awake()
        {
            _pauseWindow = FindAnyObjectByType<PauseWindow>();
            
            menuButton.onClick.AddListener(() => _pauseWindow.Pause(true));
            continueButton.onClick.AddListener(() => _pauseWindow.Pause(false));
        }
    }
}
