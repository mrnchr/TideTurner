using System.Collections;
using UnityEngine;

public class CameraMovement : MonoBehaviour, ILevelUpdatable, IUpdatable
{
    [Range(0, 1)] [SerializeField] private float _screenRate;
    [Range(0, 5)] [SerializeField] private float _speed = 2;

    [Range(0.001f, 0.01f)][SerializeField] private float _approximation;

    private const float HALF = 1f / 2f;
    private Boat _boat;
    private Vector3 _position;
    private Camera _camera;
    private Coroutine _coroutine;

    public void Construct(Boat boat)
    {
        _boat = boat;
        _camera = GetComponent<Camera>();
    }

    public void Init()
    {
        _position = transform.position;
        SetPositionY(_boat.transform.position.y);
    }

    public void SetPositionY(float value)
    {
        _position.y = value;
        transform.position = _position;
    }

    public void UpdateLogic()
    {
        if (_coroutine == null && IsOutOfRate())
            _coroutine = StartCoroutine(MoveToBoat());
    }

    private bool IsOutOfRate()
    {
        float rate = GetViewRate();
        return rate > _screenRate || rate < -_screenRate;
    }

    private float GetViewRate()
    {
        var view = _camera.WorldToViewportPoint(_boat.transform.position);
        Vector2 view2d = view;
        view2d -= Vector2.one * HALF;
        return view2d.y / HALF;
    }

    private IEnumerator MoveToBoat()
    {
        float diffY = _boat.transform.position.y - transform.position.y;
        while (Mathf.Abs(diffY) > _approximation)
        {
            var posY = transform.position.y + Mathf.Sign(diffY) * Mathf.Clamp(_speed * Time.deltaTime, 0, Mathf.Abs(diffY));
            SetPositionY(posY);
            yield return null;
            diffY = _boat.transform.position.y - transform.position.y;
        }

        SetPositionY(_boat.transform.position.y);
        _coroutine = null;
    }
}