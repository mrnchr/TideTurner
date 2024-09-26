using Muchachos.TideTurner.Runtime.Level.Savings;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Muchachos.TideTurner.Runtime.Level.Buoy
{
    public class BuoySwitcher : MonoBehaviour
    {
        [SerializeField] private Light2D _light;

        [SerializeField] private CheckPoint _checkPoints;

        private void Awake()
        {
            _light.enabled = false;

            _checkPoints.OnChecked += EnableLight;
        }

        private void EnableLight()
        {
            _light.enabled = true;
        }

        private void OnDestroy()
        {
            _checkPoints.OnChecked -= EnableLight;
        }
    }
}