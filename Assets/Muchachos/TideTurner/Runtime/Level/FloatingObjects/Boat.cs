using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.FloatingObjects
{
    public class Boat : MonoBehaviour, ILevelUpdatable, IUpdatable
    {
        [SerializeField] private FloatingObject[] floating;
        [SerializeField] private Transform _centerOfMass;
        [SerializeField] private bool _inWater;
        [SerializeField] private SoundPlayer _sound;

        [Range(30,180)][SerializeField] private int deathAngle = 45;
        [Range(1, 2)][SerializeField] private float deathHeight = 1;
        [Range(2, 5)][SerializeField] private float deathGravity = 3;

        private BoatSpawn _spawn;
        private AbstractMoonData _moon;
        private Rigidbody2D _rb;
        private WaterMovement _waterMovement;
        private global::Muchachos.TideTurner.Runtime.Level.Level _level;
        private bool _isReset;
        private void Awake()
        {
            _level = FindAnyObjectByType<global::Muchachos.TideTurner.Runtime.Level.Level>();
            _rb = GetComponent<Rigidbody2D>();
            _rb.drag = 1;
            _rb.angularDrag = 1;
            _rb.centerOfMass = _centerOfMass.position;
        }

        public void Construct(AbstractMoonData moon, BoatSpawn spawn, WaterMovement waterMovement)
        {
            _moon = moon;
            _spawn = spawn;
            _waterMovement = waterMovement;
        }

        public void Init()
        {
            SetPosition(_spawn.transform.position);

            ResetLogic();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void ResetLogic()
        {
            _rb.gravityScale = 1;
            _rb.rotation = 0;
            _rb.angularVelocity = 0;
            _rb.velocity = Vector2.zero;
            transform.eulerAngles = new Vector3(0, 0, 0);
            _isReset = true;
        }

        public void UpdateLogic()
        {
            CheckInWater();
            Kill();
        
            foreach (var floatingObject in floating)
                floatingObject.SetVelocityRate(_inWater ? _moon.MoonPosition : 0);
        }

        private void Kill()
        {
            if (_isReset == false)
                return;

            if (Vector3.Angle(Vector3.up, transform.up) > deathAngle ||
                _waterMovement.GetWaterLevel().position.y - deathHeight > transform.position.y)
            {
                _level.Lose();
                _isReset = false;
            }
        }

        private void CheckInWater()
        {
            _inWater = _waterMovement.GetWaterLevel().position.y > transform.position.y;
        }

        public void SetLoseState()
        {
            _rb.gravityScale = deathGravity;
            _sound.SetSoundState(SoundState.Play);
        }
    }
}