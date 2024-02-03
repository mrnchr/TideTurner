using UnityEngine;

namespace DefaultNamespace.Level
{
    [RequireComponent(typeof(FloatingObject))]
    public class Boat : MonoBehaviour
    {
        private Moon _moon;
        private FloatingObject _floating;

        public void Construct()
        {
            _moon = FindAnyObjectByType<Moon>();
            _floating = FindAnyObjectByType<FloatingObject>();
        }

        private void Update()
        {
            _floating.SetVelocityRate(_moon.MoonPosition);
        }
    }
}