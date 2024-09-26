using Muchachos.TideTurner.Runtime.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Muchachos.TideTurner.Runtime.Mobile
{
    public class MobileMenu : MonoBehaviour
    {
        [SerializeField] private Button menuButton;
        [SerializeField] private Button continueButton;
        [SerializeField] private PauseWindow pauseWindow;

        private void Awake()
        {
            menuButton.onClick.AddListener(() => pauseWindow.Pause(true));
            continueButton.onClick.AddListener(() => pauseWindow.Pause(false));
        }
    }
}