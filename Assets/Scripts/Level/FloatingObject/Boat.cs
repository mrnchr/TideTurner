﻿using UnityEngine;

public class Boat : MonoBehaviour, ILevelUpdatable, IUpdatable
{
    [SerializeField] private FloatingObject[] floating;

    [SerializeField] private bool _inWater;

    [Range(30,180)][SerializeField] private int deathAngle = 45;

    private BoatSpawn _spawn;
    private MoonData _moon;
    private Rigidbody2D _rb;
    private WaterMovement _waterMovement;
    private Level _level;
    private void Awake()
    {
        _level = FindAnyObjectByType<Level>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.drag = 1;
        _rb.angularDrag = 1;
    }

    public void Construct(MoonData moon, BoatSpawn spawn, WaterMovement waterMovement)
    {
        _moon = moon;
        _spawn = spawn;
        _waterMovement = waterMovement;
    }

    public void Init()
    {
        transform.position = _spawn.transform.position;
        transform.eulerAngles = Vector3.zero;

        _rb.gravityScale = 1;
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
        if ( Vector3.Angle(Vector3.up,transform.up) > deathAngle)
        {
            _level.Lose();
            Debug.Log("Lose: " + Vector3.Angle(Vector3.up, transform.forward));
        }
    }

    private void CheckInWater()
    {
        _inWater = _waterMovement.GetWaterLevel().position.y > transform.position.y;
    }

    public void SetLoseState()
    {
        _rb.gravityScale = 5;
    }
}

/*
private void LimitRotation()
{
    Vector3 euler = transform.eulerAngles;

    if (euler.z > 180)
        euler.z -= 360;

    euler.z = Mathf.Clamp(euler.z, -25, 25);
    //transform.eulerAngles = Vector3.zero;
    //transform.eulerAngles = euler;
}
*/