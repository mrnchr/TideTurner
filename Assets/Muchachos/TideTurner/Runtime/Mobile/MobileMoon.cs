using Muchachos.TideTurner.Runtime.Core.Input;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Mobile
{
    public class MobileMoon : AbstractMoon, IUpdatable
    {
        [SerializeField]
        private Slider slider;

        private RawMobileInputData _rawInputData;

        [Inject]
        public void Construct(RawMobileInputData rawInputData)
        {
            _rawInputData = rawInputData;
        }
    
        public override void Init()
        {
            transform.localScale = Vector3.one;
        }

        public void UpdateLogic()
        {
            _rawInputData.VerticalMovement = slider.value;
        }
    }
}