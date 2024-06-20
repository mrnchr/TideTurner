using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class ButtonSoundCaller : MonoBehaviour
    {
        private IButtonSoundPlayer _player;

        [Inject]
        public void Construct(IButtonSoundPlayer player)
        {
            _player = player;
        }

        public void PlaySound()
        {
            _player.PlaySound();
        }
    }
}