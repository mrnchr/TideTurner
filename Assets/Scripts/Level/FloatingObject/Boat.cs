using UnityEngine;

public class Boat : MonoBehaviour, ILevelUpdatable, IUpdatable
{
    [SerializeField] private FloatingObject[] floating;
    [SerializeField] private Transform _centerOfMass;
    [SerializeField] private bool _inWater;

    [Range(30,180)][SerializeField] private int deathAngle = 45;

    private BoatSpawn _spawn;
    private MoonData _moon;
    private Rigidbody2D _rb;
    private WaterMovement _waterMovement;
    private Level _level;
    private bool _isReset = false;
    private void Awake()
    {
        _level = FindAnyObjectByType<Level>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.drag = 1;
        _rb.angularDrag = 1;
        _rb.centerOfMass = _centerOfMass.position;
    }

    public void Construct(MoonData moon, BoatSpawn spawn, WaterMovement waterMovement)
    {
        _moon = moon;
        _spawn = spawn;
        _waterMovement = waterMovement;
    }

    public void Init()
    {
        transform.position = _spawn.transform.position;

        _rb.gravityScale = 1;
        _rb.rotation = 0;
        _rb.angularVelocity = 0;
        _rb.velocity = Vector2.zero;
        transform.eulerAngles = new Vector3(0, 0, 0);
        _isReset = true;
    }

    public void UpdateLogic()
    {
        CheckInWater();
        Kill();
        
        foreach (var floatingObject in floating)
            floatingObject.SetVelocityRate(_inWater ? _moon.MoonPosition : 0);
    }

    private void Kill()
    {
        if ( Vector3.Angle(Vector3.up,transform.up) > deathAngle && _isReset == true)
        {
            _level.Lose();
            _isReset = false;
            //Debug.Log("Lose: " + Vector3.Angle(Vector3.up, transform.forward));
        }
    }

    private void CheckInWater()
    {
        _inWater = _waterMovement.GetWaterLevel().position.y > transform.position.y;
    }

    public void SetLoseState()
    {
        _rb.gravityScale = 5;
    }
}