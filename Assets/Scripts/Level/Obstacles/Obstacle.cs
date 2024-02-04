using System;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public event Action OnPlayerCollision;

    private Level _level;
    public void Construct(Level level)
    {
        _level = level;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Boat") == false)
            return;

        OnPlayerCollision?.Invoke();
        _level.Lose();
    }
}