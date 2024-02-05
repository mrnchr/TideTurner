using UnityEngine;
using UnityEngine.UI;

public class Moon : MonoBehaviour, ILevelUpdatable, IUpdatable
{
    [SerializeField] private Slider _slider;
    [SerializeField] private Vector2 BoundMoonSize;
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
        _slider.value = _data.MoonPosition;
    }
}