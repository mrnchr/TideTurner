using UnityEngine.Audio;

namespace Muchachos.TideTurner.Runtime.UI
{
    public interface IAudioMixerProvider
    {
        AudioMixer Mixer { get; }
    }
}