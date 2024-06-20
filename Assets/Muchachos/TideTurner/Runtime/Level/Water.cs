using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class Water : MonoBehaviour, ILevelUpdatable, IUpdatable
    {
        [SerializeField] private SoundPlayer _sound;
    
        private AbstractMoonData _moon;

        public WaterMovement Movement { get; private set; }

        public void Construct(AbstractMoonData moon)
        {
            _moon = moon;
            Movement = FindAnyObjectByType<WaterMovement>();
        }

        public void Init()
        {
            Movement.Init();
            _sound.SetSoundState(SoundState.Play);
        }
        
        public void UpdateLogic()
        {
            Movement.ChangeWaterLevel(_moon.MoonSize);
        }
    }
}