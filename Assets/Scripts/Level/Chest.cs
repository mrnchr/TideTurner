using UnityEngine;

public class Chest : MonoBehaviour
{
    private Level _level;
    private void Awake()
    {
        _level = FindAnyObjectByType<Level>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boat") == false)
            return;

        _level.Win();
    }
}
