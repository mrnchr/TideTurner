using Muchachos.TideTurner.Runtime.Level.Savings;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Muchachos.TideTurner.Runtime.Level.Buoy
{
    public class BuoySwitcher : MonoBehaviour
    {
        private static readonly int _float = Animator.StringToHash("Float");

        [SerializeField]
        private Light2D _light;

        [SerializeField]
        private CheckPoint _checkPoints;

        private void Start()
        {
            _light.enabled = false;
        }

        private void Update()
        {
            if (_checkPoints.IsChecked)
                _light.enabled = true;
        }
    }
}