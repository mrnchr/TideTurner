using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private bool isSound3D;

    public bool IsPlaying => _sound.isPlaying;
    public float SoundLength => _sound.clip.length;

    private AudioSource _sound;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();
        _sound.spatialBlend = isSound3D ? 1 : 0;
    }

    public void SetVolume(float volume)
    {
        _sound.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    public void SetSoundState(SoundState state)
    {
        switch (state)
        {
            case SoundState.Play:
                _sound.Play();
                break;
            case SoundState.Pause:
                _sound.Pause();
                break;
            case SoundState.Stop:
                _sound.Stop();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }
}

public enum SoundState
{
    Play = 0,
    Pause = 1,
    Stop = 2
}