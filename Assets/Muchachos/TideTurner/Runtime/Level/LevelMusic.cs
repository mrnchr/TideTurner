using Muchachos.TideTurner.Runtime.Core;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class LevelMusic : MonoBehaviour
    {
        [SerializeField] private SoundPlayer _sound;

        public void SetSound(SoundState state)
        {
            _sound.SetSoundState(state);
        }
    }
}