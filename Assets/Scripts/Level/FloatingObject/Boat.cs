using UnityEngine;

namespace DefaultNamespace.Level
{
    public class Boat : MonoBehaviour, ILevelUpdatable
    {
        [SerializeField] private FloatingObject[] floating;
        private BoatSpawn _spawn;

        private Moon _moon;
        private Rigidbody _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            
            _rb.drag = 1;
            _rb.angularDrag = 1;
            _rb.useGravity = false;

            _rb.constraints = RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationY |
                             RigidbodyConstraints.FreezePositionZ;
        }

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

            LimitRotation();
        }

        private void LimitRotation()
        {
            Vector3 euler = transform.eulerAngles;
            
            if (euler.z > 180) 
                euler.z = euler.z - 360;
            
            euler.z = Mathf.Clamp(euler.z, -25, 25);
            transform.eulerAngles = euler;
        }
    }
}