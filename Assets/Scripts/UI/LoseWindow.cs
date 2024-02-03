using UnityEngine;

namespace DefaultNamespace.UI
{
    public class LoseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        
        public void SetActive(bool value)
        {
            _window.SetActive(value);
        }
    }
}