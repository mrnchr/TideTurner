using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;

public class CameraScaler : MonoBehaviour
{
    private const float Portraitfov = 12f, Normalfov = 5.4f;
    
    [SerializeField] private Vector2 _defaultResolution;
    [Range(0f, 1f)] [SerializeField] private float _widthOrHeight;

    private Camera _camera;
    private float _initialSize;
    private float _targetAspect;
    private float _initialFov;
    private float _horizontalFov = 120f;
    
    private void Start()
    {
        _camera = GetComponent<Camera>();
        
        _initialSize = _camera.orthographicSize;
        _targetAspect = _defaultResolution.x / _defaultResolution.y;
        _initialFov = _camera.fieldOfView;
        _horizontalFov = CalculateVerticalFov(_initialFov, 1 / _targetAspect);
        SetSize();
    }
    
    public void ChangeOrthographicSize(OrthographicSizeType type)
    {
        return;
        
        switch (type)
        {
            case OrthographicSizeType.PORTAIT:
                _camera.orthographicSize = Portraitfov;
                break;
            default:
                _camera.orthographicSize = Normalfov;
                break;
        }
    }

    private void SetSize()
    {
        if (_camera.orthographic)
        {
            float constantWidthSize = _initialSize * (_targetAspect / _camera.aspect);
            _camera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, _widthOrHeight);
            return;
        }
        
        float constantWidthFov = CalculateVerticalFov(_horizontalFov, _camera.aspect);
        _camera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, _widthOrHeight);
    }

    private void Update()
    {
        SetSize();
    }

    private float CalculateVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;
        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);
        return vFovInRads * Mathf.Rad2Deg;
    }
}