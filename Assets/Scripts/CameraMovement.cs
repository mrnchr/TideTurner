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
    private Camera _childCamera;
    private Coroutine _coroutine;
    private const float PORTRAITFOV = 12f, OTHERFOV = 5.4f;
    public void Construct(Boat boat)
    {
        _boat = boat;
        _camera = GetComponent<Camera>();
        _childCamera = transform.GetChild(0).GetComponent<Camera>();
    }

    public void Init()
    {
        _position = transform.position;
        SetPositionY(_boat.transform.position.y);
    }

    private void SetPositionY(float value)
    {
        _position.y = value;
        transform.position = _position;
    }

    public void UpdateLogic()
    {
        if (_coroutine == null && IsOutOfRate())
            _coroutine = StartCoroutine(MoveToBoat());
    }

    private void HandleOrientationData()
    {
        if (Screen.orientation == ScreenOrientation.Portrait && Application.isMobilePlatform == true)
        {
            _camera.orthographicSize = PORTRAITFOV;
            _childCamera.orthographicSize = PORTRAITFOV;
            
        }
        else if (Screen.orientation != ScreenOrientation.Portrait)
        {
            _camera.orthographicSize = OTHERFOV;
            _childCamera.orthographicSize = OTHERFOV;
        }
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