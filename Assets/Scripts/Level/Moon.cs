﻿using UnityEngine;

public class Moon : MonoBehaviour, ILevelUpdatable
{
    public float MoonSize;
    public float MoonPosition;

    [SerializeField] private float _defaultMoonSize;
    [SerializeField] private float _defaultMoonPosition;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private float SizeSpeed;
    [SerializeField] private Vector2 BoundMoonSize;
    [SerializeField] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;
    private InputController _input;
    private float _moonPosition;
    private float _moonSize;

    public void Construct()
    {
        Debug.Log("Moon Construct");
        _input = FindAnyObjectByType<InputController>();
        _input.OnInputHandled += Move;
    }

    private void OnDestroy()
    {
        _input.OnInputHandled -= Move;
    }

    public void Init()
    {
        MoonPosition = _defaultMoonPosition;
        _moonPosition = MoveLerp(MoonPosition);
        SetPosition();

        MoonSize = _defaultMoonSize;
        _moonSize = SizeLerp(MoonSize);
        SetSize();
    }

    private void Move(InputData data)
    {
        Debug.Log("Move");
        MoonPosition += data.MouseDeltaX * MoveSpeed;
        MoonPosition = Mathf.Clamp(MoonPosition, -1, 1);
        _moonPosition = MoveLerp(MoonPosition);

        MoonSize += data.WheelDelta * SizeSpeed;
        MoonSize = Mathf.Clamp(MoonSize, 0, 1);
        _moonSize = SizeLerp(MoonSize);
    }

    public float MoveLerp(float value)
    {
        return Mathf.Lerp(_leftBound.position.x, _rightBound.position.x, (value + 1) / 2);
    }

    public float SizeLerp(float value)
    {
        return Mathf.Lerp(BoundMoonSize.x, BoundMoonSize.y, value);
    }

    public void UpdateLogic()
    {
        SetPosition();
        SetSize();
    }

    private void SetSize()
    {
        transform.localScale = new Vector3(1, 1, 1) * _moonSize;
    }

    private void SetPosition()
    {
        Vector3 pos = transform.position;
        pos.x = _moonPosition;
        transform.position = pos;
    }
}