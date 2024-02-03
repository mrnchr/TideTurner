﻿using System.Collections.Generic;
using UnityEngine;

public class BallPool : MonoBehaviour
{
    private readonly List<Ball> _balls = new List<Ball>();
    [SerializeField] private Transform _ballParent;
    [SerializeField] private Ball _prefab;
    [SerializeField] private int _size;
    private BallFactory _factory;

    public void Construct()
    {
        _factory = new BallFactory(_prefab, _ballParent);
    }

    public void Init()
    {
        for (int i = 0; i < _size; i++)
        {
            var instance = _factory.Create();
            instance.SetPool(this);
            Push(instance);
            _balls.Add(instance);
        }
    }

    public void Clear()
    {
        foreach (Ball ball in _balls)
            Push(ball);
    }

    public void Push(Ball ball)
    {
        ball.gameObject.SetActive(false);
        ball.SetVelocity(Vector3.zero);
        ball.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }
        
    public Ball Pop(Transform spawn, float velocity)
    {
        var instance = _balls.Find(x => !x.gameObject.activeSelf);
        instance.transform.position = spawn.position;
        instance.transform.right = spawn.right;
        instance.SetVelocity(velocity * spawn.right);
        instance.gameObject.SetActive(true);
        return instance;
    }
        
}