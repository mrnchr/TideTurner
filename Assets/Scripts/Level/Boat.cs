using UnityEngine;

namespace DefaultNamespace.Level
{
    [RequireComponent(typeof(FloatingObject))]
    public class Boat : MonoBehaviour, ILevelUpdatable
    {
        private Moon _moon;
        private FloatingObject _floating;
        private BoatSpawn _spawn;

        public void Construct()
        {
            _moon = FindAnyObjectByType<Moon>();
            _floating = FindAnyObjectByType<FloatingObject>();
            _spawn = FindAnyObjectByType<BoatSpawn>();
        }

        public void Init()
        {
            transform.position = _spawn.transform.position;
        }

        public void UpdateLogic()
        {
            _floating.SetVelocityRate(_moon.MoonPosition);
        }
    }
}