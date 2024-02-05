using UnityEngine;

namespace DefaultNamespace.UI
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