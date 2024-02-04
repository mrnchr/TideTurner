using UnityEngine;

public class Barrel : MonoBehaviour, ILevelUpdatable
{
    [SerializeField] private FloatingObject[] floating;
    [SerializeField] private SoundPlayer boomSound;
    [SerializeField] private ParticleSystem boomEffect;

    private bool _inWater;
    private MoonData _moon;
    private Obstacle _obstacle;
    private Rigidbody2D rb;
    
    public void Construct(MoonData moon)
    {
        _obstacle = GetComponent<Obstacle>();
        rb = GetComponent<Rigidbody2D>();
        _moon = moon;

        _obstacle.OnPlayerCollision += SubsribeToObstacle;
    }

    public void Init()
    {
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector2.zero;
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
