using System;
using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Level.Savings;
using Muchachos.TideTurner.Runtime.Physics;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level.FloatingObjects
{
    public class Boat : MonoBehaviour, ILevelUpdatable, IUpdatable
    {
        [SerializeField] private SoundPlayer _sound;

        [Range(30, 180)] [SerializeField] private int deathAngle = 45;

        [Range(1, 2)] [SerializeField] private float deathHeight = 1;

        [Range(2, 5)] [SerializeField] private float deathGravity = 3;

        [SerializeField] private float _deathForce;

        [SerializeField] private FloatingBody _body;

        public Action OnLose;

        private BoatSpawn _spawn;
        private Rigidbody2D _rb;
        private WaterMovement _waterMovement;
        private CheckPointHandler _handler;
        private UserData _userData;
        private bool _isReset;
        
        [Inject]
        public void Construct(BoatSpawn spawn, WaterMovement waterMovement, 
            CheckPointHandler handler, UserData userData)
        {
            _spawn = spawn;
            _waterMovement = waterMovement;
            _handler = handler;
            _userData = userData;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.drag = 1;
            _rb.angularDrag = 1;
        }

        public void Init()
        {
            var spawnPos = _handler.GetSpawnPosition();

            SetPosition(_userData.CurrentInd < 0 ? _spawn.transform.position : spawnPos);

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
            Kill();
        }

        private void Kill()
        {
            if (!_isReset)
                return;

            if (Vector3.Angle(Vector3.up, transform.up) > deathAngle ||
                _waterMovement.GetWaterLevel().position.y - deathHeight > transform.position.y)
            {
                OnLose?.Invoke();
                _isReset = false;
            }
        }

        public void SetLoseState()
        {
            _rb.AddForceAtPosition(Vector3.up * _deathForce, _body.Floatings[0].transform.position,
                ForceMode2D.Impulse);
            _rb.gravityScale = deathGravity;
            _sound.SetSoundState(SoundState.Play);
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!_waterMovement)
                return;
            
            var leftCorner = new Vector2(-100, _waterMovement.GetWaterLevel().position.y - deathHeight);
            var rightCorner = new Vector2(100, _waterMovement.GetWaterLevel().position.y - deathHeight);
            Debug.DrawLine(leftCorner, rightCorner, Color.red);
        }
        #endif
    }
}