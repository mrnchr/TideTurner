using UnityEngine;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class ButtonSoundCaller : MonoBehaviour
    {
        private ButtonSoundPlayer _player;

        public void Construct()
        {
            FindPlayer();
        }

        private void FindPlayer()
        {
            _player = FindAnyObjectByType<ButtonSoundPlayer>();
        }

        public void PlaySound()
        {
            _player.PlaySound();
        }
    }
}