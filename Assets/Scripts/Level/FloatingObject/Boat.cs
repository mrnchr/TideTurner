using UnityEngine;

namespace DefaultNamespace.Level
{
    public class Boat : MonoBehaviour, ILevelUpdatable
    {
        private Moon _moon;
        [SerializeField] private FloatingObject[] floating;
        private BoatSpawn _spawn;

        public void Construct()
        {
            _moon = FindAnyObjectByType<Moon>();
            _spawn = FindAnyObjectByType<BoatSpawn>();
        }

        public void Init()
        {
            transform.position = _spawn.transform.position;
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