using System.Linq;
using Muchachos.TideTurner.Runtime.Level;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Mobile;
using TriInspector;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Physics
{
    public class FloatingBody : MonoBehaviour, IFixedUpdatable, ILevelUpdatable
    {
        public FloatingPoint[] Floatings;

        [SerializeField]
        private Rigidbody2D _rb;

        [SerializeField]
        private Transform _centerOfMass;

        [SerializeField]
        private float _height;

        [SerializeField]
        private float _lightness;

        private AbstractMoonData _moon;

        private bool _inWater;

        private WaterMovement _waterMovement;

        public float Height => _height;

        public Rigidbody2D Rb => _rb;

        public float RiverFlow => _moon.MoonPosition;

        public bool InWater => _inWater;

        [Inject]
        public void Construct(ILevelUpdater updater)
        {
            updater.Add(this);
        }

        public void Awake()
        {
            _moon = FindAnyObjectByType<AbstractMoonData>();
            _waterMovement = FindAnyObjectByType<WaterMovement>();
        }

        private void Start()
        {
            if (_centerOfMass)
                Rb.centerOfMass = _centerOfMass.localPosition;
        }

        public float GetVolumeRate(Vector3 position)
        {
            float waterLevel = _waterMovement.GetMeshWaterLevel(position.x + Rb.velocity.x * Time.fixedDeltaTime);

            // distance between water level and bottom point of object
            float k = waterLevel - (position.y - Height / 2);
            // rate of floated in water body based on its height
            // >= 1 - there is body full in water
            // <= 0 - there is body above the water
            return Mathf.Clamp(k / Height, 0, 1);
        }

        public void FixedUpdateLogic()
        {
            if (enabled)
                UpdateFloating();
        }

        private void UpdateFloating()
        {
            foreach (FloatingPoint floating in Floatings)
                floating.UpdateFloatingForce();

            _inWater = Floatings.Any(x => x.InWater);
            if (InWater)
                Rb.AddForce(Vector2.right * (RiverFlow * _lightness), ForceMode2D.Force);

            ClampHorizontalVelocity();
        }

        private void ClampHorizontalVelocity()
        {
            Vector2 velocity = Rb.velocity;
            velocity.x = Mathf.Clamp(velocity.x, -_lightness, _lightness);
            Rb.velocity = velocity;
        }

#if UNITY_EDITOR
        [SerializeField]
        [Title("Gizmo")]
        private Transform _centerOfVolume;

        private Vector3 CenterPosition => _centerOfVolume ? _centerOfVolume.position : transform.position;

        private void OnDrawGizmos()
        {
            Color color = Color.blue;
            color.a = 0.5f;
            Gizmos.color = color;
            Gizmos.DrawCube(CenterPosition, new Vector3(1, _height, 1));
        }
#endif
    }
}