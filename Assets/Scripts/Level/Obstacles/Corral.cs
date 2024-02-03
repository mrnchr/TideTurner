using DefaultNamespace.Level;
using UnityEngine;

public class Corral : MonoBehaviour
{
    private Level _level;

    public void Construct(Level level)
    {
        _level = level;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Boat _))
        {
            _level.Lose();
        }
    }
}