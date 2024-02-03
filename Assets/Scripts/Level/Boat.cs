using UnityEngine;

namespace DefaultNamespace.Level
{
    [RequireComponent(typeof(FloatingObject))]
    public class Boat : MonoBehaviour, ILevelUpdatable
    {
        private Moon _moon;
        private FloatingObject _floating;

        public void Construct()
        {
            _moon = FindAnyObjectByType<Moon>();
            _floating = FindAnyObjectByType<FloatingObject>();
        }

        public void UpdateLogic()
        {
            _floating.SetVelocityRate(_moon.MoonPosition);
        }
    }
}