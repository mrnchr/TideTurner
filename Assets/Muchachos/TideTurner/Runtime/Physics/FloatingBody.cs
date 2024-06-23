using System.Linq;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Physics
{
    public class FloatingBody : MonoBehaviour, IFixedUpdatable, ILevelUpdatable
    {
        [SerializeField]
        private FloatingPoint[] _floatings;

        [SerializeField]
        private Rigidbody2D _rb;

        [SerializeField]
        private Transform _centerOfMass;

        [SerializeField]
        private float _height;

        [SerializeField]
        private float _lightness;

        private AbstractMoonData _moon;

        public float Height => _height;
        public Rigidbody2D Rb => _rb;
        public float RiverFlow => _moon.MoonPosition;

        [Inject]
        public void Construct(ILevelUpdater updater)
        {
            updater.Add(this);
        }

        public void Awake()
        {
            _moon = FindAnyObjectByType<AbstractMoonData>();
        }

        private void Start()
        {
            if (_centerOfMass)
                Rb.centerOfMass = _centerOfMass.localPosition;
        }

        public void FixedUpdateLogic()
        {
            UpdateFloating();
        }

        private void UpdateFloating()
        {
            foreach (FloatingPoint floating in _floatings)
                floating.UpdateFloatingForce();

            if (_floatings.Any(x => x.InWater))
                Rb.AddForce(Vector2.right * (RiverFlow * _lightness), ForceMode2D.Force);

            ClampHorizontalVelocity();
        }

        private void ClampHorizontalVelocity()
        {
            Vector2 velocity = Rb.velocity;
            velocity.x = Mathf.Clamp(velocity.x, -_lightness, _lightness);
            Rb.velocity = velocity;
        }
    }
}