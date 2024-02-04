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
    private WaterMovement _waterMovement;

    public void Construct(MoonData moon, WaterMovement waterMovement)
    {
        _obstacle = GetComponent<Obstacle>();
        rb = GetComponent<Rigidbody2D>();
        _moon = moon;
        _waterMovement = waterMovement;

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
        CheckInWater();
        
        foreach (var floatingObject in floating)
            floatingObject.SetVelocityRate(_inWater ? _moon.MoonPosition : 0);
    }

    private void CheckInWater()
    {
        _inWater = _waterMovement.GetWaterLevel().position.y > transform.position.y;
    }
}
