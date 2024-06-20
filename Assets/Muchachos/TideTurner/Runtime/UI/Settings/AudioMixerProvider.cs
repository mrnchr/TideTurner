using UnityEngine.Audio;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class AudioMixerProvider : IAudioMixerProvider
    {
        public AudioMixer Mixer { get; }

        public AudioMixerProvider(AudioMixer mixer)
        {
            Mixer = mixer;
        }
    }
}