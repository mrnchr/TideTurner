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

        [SerializeField] private FloatingBody _body;
        
        [SerializeField] private BoatConfig _boatConfig;

        public Action OnLose;

        private Rigidbody2D _rb;
        private WaterMovement _waterMovement;
        private CheckPointHandler _handler;
        private bool _isReset;
        
        [Inject]
        public void Construct(
            WaterMovement waterMovement, 
            CheckPointHandler handler)
        {
            _waterMovement = waterMovement;
            _handler = handler;
        }

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.drag = 1;
            _rb.angularDrag = 1;
        }

        public void Init()
        {
            Vector3 spawnPos = _handler.GetSpawnPosition();

            SetPosition(spawnPos);

            Reset();
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void UpdateLogic()
        {
            Kill();
        }

        private void Kill()
        {
            if (!_isReset)
                return;

            if (Vector3.Angle(Vector3.up, transform.up) > _boatConfig.DeathAngle ||
                _waterMovement.GetWaterLevel().position.y - _boatConfig.DeathHeight > transform.position.y)
            {
                OnLose?.Invoke();
                _isReset = false;
            }
        }

        public void SetLoseState()
        {
            _rb.AddForceAtPosition(Vector3.up * _boatConfig.DeathForce, _body.Floatings[0].transform.position,
                ForceMode2D.Impulse);
            _rb.gravityScale = _boatConfig.DeathGravity;
            _sound.SetSoundState(SoundState.Play);
        }
        
        public void Reset()
        {
            _rb.gravityScale = 1;
            _rb.rotation = 0;
            _rb.angularVelocity = 0;
            _rb.velocity = Vector2.zero;
            transform.eulerAngles = new Vector3(0, 0, 0);
            _isReset = true;
        }

        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (!_waterMovement)
                return;
            
            Vector2 leftCorner = new Vector2(-100, _waterMovement.GetWaterLevel().position.y - _boatConfig.DeathHeight);
            Vector2 rightCorner = new Vector2(100, _waterMovement.GetWaterLevel().position.y - _boatConfig.DeathHeight);
            Debug.DrawLine(leftCorner, rightCorner, Color.red);
        }
        #endif
    }
}