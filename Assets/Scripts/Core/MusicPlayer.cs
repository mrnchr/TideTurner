using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private MusicType type;

    public MusicType MusicType => type;

    private AudioSource _music;
    private SoundPlayer _sound;

    private void Awake()
    {
        _sound = GetComponent<SoundPlayer>();
    }

    public void SetState(SoundState state)
    {
        _sound.SetSoundState(state);
    }
}

public enum MusicType
{
    Menu,
    Level
}