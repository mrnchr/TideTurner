using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;
using UnityEngine.UI;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class Moon : AbstractMoon, IUpdatable
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Vector2 BoundMoonSize;
        private MoonData _data;

        public void Construct(MoonData data)
        {
            _data = data;
        }

        public override void Init()
        {
            SetPosition();
            SetSize();
        }

        private float SizeLerp(float value)
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
}