using Muchachos.TideTurner.Runtime.Core;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class ButtonSoundPlayer : MonoBehaviour
    {
        [SerializeField] private SoundPlayer _sound;

        public void PlaySound()
        {
            _sound.SetSoundState(SoundState.Play);
        }
    }
}