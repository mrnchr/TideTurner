using UnityEngine;

namespace DefaultNamespace.UI
{
    public class WinWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        
        public void SetActive(bool value)
        {
            _window.SetActive(value);
        }
    }
}