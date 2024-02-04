using UnityEngine;

public class Boat : MonoBehaviour, ILevelUpdatable
{
    [SerializeField] private FloatingObject[] floating;
    private BoatSpawn _spawn;

    private MoonData _moon;
    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

        _rb.drag = 1;
        _rb.angularDrag = 1;

        //_rb.gravityScale
    }

    public void Construct(MoonData moon, BoatSpawn spawn)
    {
        _moon = moon;
        _spawn = spawn;
    }

    public void Init()
    {
        transform.position = _spawn.transform.position;
        transform.eulerAngles = Vector3.zero;
    }

    public void UpdateLogic()
    {
        foreach (var floatingObject in floating)
            floatingObject.SetVelocityRate(_moon.MoonPosition);

        LimitRotation();
    }

    private void LimitRotation()
    {
        Vector3 euler = transform.eulerAngles;

        if (euler.z > 180)
            euler.z -= 360;

        euler.z = Mathf.Clamp(euler.z, -25, 25);
        //transform.eulerAngles = Vector3.zero;
        //transform.eulerAngles = euler;
    }
}