using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform _ballSpawn;
    [SerializeField] private float _ballSpeed;
    [SerializeField] private float _shotDelay;
    private BallPool _pool;

    public void Construct(BallPool pool)
    {
        _pool = pool;
    }

    public void Init()
    {
        StartCoroutine(ShotRoutine());
    }

    private IEnumerator ShotRoutine()
    {
        while (true)
        {
            _pool.Pop(_ballSpawn, _ballSpeed);
            yield return new WaitForSeconds(_shotDelay);
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}