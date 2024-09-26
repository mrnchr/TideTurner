using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Level.Obstacles.LifeCycle;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class Water : MonoBehaviour, ILevelUpdatable, IUpdatable
    {
        [SerializeField] private SoundPlayer _sound;
    
        private AbstractMoonData _moon;

        public WaterMovement Movement { get; private set; }

        [Inject]
        public void Construct(AbstractMoonData moon, WaterMovement waterMovement)
        {
            _moon = moon;
            Movement = waterMovement;
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