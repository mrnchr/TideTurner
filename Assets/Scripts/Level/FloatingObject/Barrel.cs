using UnityEngine;

public class Barrel : MonoBehaviour, ILevelUpdatable
{
    [SerializeField] private FloatingObject[] floating;
    [SerializeField] private SoundPlayer boomSound;
    [SerializeField] private ParticleSystem boomEffect;

    private bool _inWater;
    private MoonData _moon;
    private Obstacle _obstacle;
    public void Construct(MoonData moon)
    {
        _obstacle = GetComponent<Obstacle>();
        _moon = moon;

        _obstacle.OnPlayerCollision += SubsribeToObstacle;
    }

    private void SubsribeToObstacle()
    {
        boomSound.SetSoundState(SoundState.Play);
        boomEffect.gameObject.transform.parent = null;
        boomEffect.Play();
        gameObject.SetActive(false);
    }

    public void UpdateLogic()
    {
        foreach (var floatingObject in floating)
            floatingObject.SetVelocityRate(_inWater ? _moon.MoonPosition : 0);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
            _inWater = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water"))
            _inWater = false;
    }
}
