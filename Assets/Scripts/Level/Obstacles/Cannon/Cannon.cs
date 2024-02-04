using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private Transform _ballSpawn;
    [Range(0f, 25f)][SerializeField] private float _ballSpeed;
    [Range(0, 2f)][SerializeField] private float _shotDelay;

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
        while (true)
        {
            if (shotSoundPlayer.IsPlaying == false)
            {
                shotSoundPlayer.SetSoundState(true);
                _pool.Pop(_ballSpawn, _ballSpeed);
                yield return new WaitForSeconds(shotSoundPlayer.SoundLength);
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}