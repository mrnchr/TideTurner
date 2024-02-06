using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform _ballSpawn;
    [Range(0f, 25f)] [SerializeField] private float _ballSpeed;
    [SerializeField] private Vector2 _startTime;
    [SerializeField] private float _shotDelay;

    [SerializeField] private SoundPlayer shotSoundPlayer;

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
        yield return new WaitForSeconds(Random.Range(_startTime.x, _startTime.y));
        while (true)
        {
            shotSoundPlayer.SetSoundState(SoundState.Play);
            _pool.Pop(_ballSpawn, _ballSpeed);

            yield return new WaitForSeconds(_shotDelay);
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}