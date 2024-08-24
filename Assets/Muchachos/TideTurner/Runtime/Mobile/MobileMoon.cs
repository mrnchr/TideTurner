using UnityEngine;
using UnityEngine.UI;

namespace Muchachos.TideTurner.Runtime.Mobile
{
    public class MobileMoon : AbstractMoon
    {
        public float GetSliderValue => _slider.value;
        
        [SerializeField] private Slider _slider;
        [SerializeField] private Canvas _canvas;
        [SerializeField] private bool _isMobileScene;
    
        public override void Init()
        {
            transform.localScale = Vector3.one;

            if (Application.isMobilePlatform || _isMobileScene)
                _canvas.enabled = true;
        }

        public void DisableCanvas()
        {
            _canvas.enabled = false;
        }
    }
}