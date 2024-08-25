using UnityEngine;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class ExitButton : MonoBehaviour
    {
        private void Awake()
        {
            if (Application.isMobilePlatform)
                gameObject.SetActive(false);
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}