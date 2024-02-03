using UnityEngine;

namespace DefaultNamespace.Level
{
    public class Boat : MonoBehaviour, ILevelUpdatable
    {
        [SerializeField] private FloatingObject[] floating;

        private Moon _moon;
        private Rigidbody _rb;
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            
            _rb.drag = 0;
            _rb.angularDrag = 0;
            _rb.useGravity = false;

            _rb.constraints = RigidbodyConstraints.FreezeRotationX |
                             RigidbodyConstraints.FreezeRotationY |
                             RigidbodyConstraints.FreezePositionZ;
        }

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