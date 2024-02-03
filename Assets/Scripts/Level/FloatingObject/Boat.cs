using UnityEngine;

namespace DefaultNamespace.Level
{
    public class Boat : MonoBehaviour, ILevelUpdatable
    {
        private Moon _moon;
        [SerializeField] private FloatingObject[] floating;

        public void Construct()
        {
            _moon = FindAnyObjectByType<Moon>();
        }

        public void UpdateLogic()
        {
            foreach (var floatingObject in floating)
            {
                floatingObject.SetVelocityRate(_moon.MoonPosition);
            }
        }
    }
}