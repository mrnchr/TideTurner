using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private Level _level;

    public void Construct(Level level)
    {
        _level = level;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Boat"))
        {
            _level.Lose();
        }
    }
}