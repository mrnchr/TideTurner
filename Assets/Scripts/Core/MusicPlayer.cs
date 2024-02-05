using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private MusicType type;

    public MusicType MusicType => type;

    private AudioSource _music;
    private void Awake()
    {
        _music = GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _music.playOnAwake = false;
        _music.spatialBlend = 0;
        _music.loop = true;
    }

    public void SetState(bool state)
    {
        switch (state)
        {
            case true:
                _music.Play();
                break;
            case false:
                _music.Stop();
                break;
        }  
    }

    public void SetVolume(float volume)
    {
        _music.volume = Mathf.Clamp( volume, 0f, 1f);
    }
}

public enum MusicType { menu, level}