using System;
using Muchachos.TideTurner.Runtime.Core.Input;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class MoonData : AbstractMoonData
    {
        [SerializeField] private float _defaultMoonSize;
        [SerializeField] private float _defaultMoonPosition;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _sizeSpeed;
    
        private InputController _input;
    
        public override void Construct()
        {
            _input = FindAnyObjectByType<InputController>();
        
            _input.OnInputHandled += Move;
        }

        private void OnDestroy()
        {
            _input.OnInputHandled -= Move;
        }
        
        public override void Init()
        {
            MoonPosition = _defaultMoonPosition;
            MoonSize = _defaultMoonSize;
        }
        
        private void Move(InputData data)
        {
            MoonPosition += data.HorizontalInput * _moveSpeed;
            MoonPosition = Mathf.Clamp(MoonPosition, -1, 1);

            if (CheckForChangeDirection(data))
                MoonSize = _defaultMoonSize;
        
            MoonSize += data.VerticalInput * _sizeSpeed;
            MoonSize = Mathf.Clamp(MoonSize, -1, 1);
        }

        private bool CheckForChangeDirection(InputData data) => data.VerticalInput != 0 && !IsEqualSign(data.VerticalInput, MoonSize);

        private bool IsEqualSign(float a, float b) => Math.Abs(Mathf.Sign(a) - Mathf.Sign(b)) < 0.0001f; 
    }
}