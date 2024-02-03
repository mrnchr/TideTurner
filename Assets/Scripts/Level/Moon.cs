using UnityEngine;

public class Moon : MonoBehaviour, ILevelUpdatable
{
    [SerializeField] private Vector2 BoundMoonSize;
    [SerializeField] private Transform _leftBound;
    [SerializeField] private Transform _rightBound;
    private MoonData _data;

    public void Construct(MoonData data)
    {
        _data = data;
    }

    public void Init()
    {
        SetPosition();
        SetSize();
    }

    public float MoveLerp(float value)
    {
        return Mathf.Lerp(_leftBound.position.x, _rightBound.position.x, (value + 1) / 2);
    }

    public float SizeLerp(float value)
    {
        return Mathf.Lerp(BoundMoonSize.x, BoundMoonSize.y, (value + 1) / 2);
    }

    public void UpdateLogic()
    {
        SetPosition();
        SetSize();
    }

    private void SetSize()
    {
        transform.localScale = new Vector3(1, 1, 1) * SizeLerp(_data.MoonSize);
    }

    private void SetPosition()
    {
        Vector3 pos = transform.position;
        pos.x = MoveLerp(_data.MoonPosition);
        transform.position = pos;
    }
}