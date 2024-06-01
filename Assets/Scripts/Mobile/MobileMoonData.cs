using System;
using UnityEngine;

public class MobileMoonData : AbstractMoonData
{
    [SerializeField] private float defaultMoonSize;
    [SerializeField] private float defaultMoonPosition;
    [SerializeField] private float moveSpeed = 1;
    [SerializeField] private float sizeSpeed = 1;

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
        MoonPosition = defaultMoonPosition;
        MoonSize = defaultMoonSize;
    }

    private void Move(InputData data)
    {
        MoonPosition += data.HorizontalInput * moveSpeed;
        MoonPosition = Mathf.Clamp(MoonPosition, -1, 1);

        if (CheckForChangeDirection(data))
            MoonSize = defaultMoonSize;

        MoonSize += data.VerticalInput * sizeSpeed;
        MoonSize = Mathf.Clamp(MoonSize, -1, 1);
    }

    private bool CheckForChangeDirection(InputData data) =>
        data.VerticalInput != 0 && !IsEqualSign(data.VerticalInput, MoonSize);

    private bool IsEqualSign(float a, float b) => Math.Abs(Mathf.Sign(a) - Mathf.Sign(b)) < 0.0001f;
}