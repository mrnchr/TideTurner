using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    private AudioSource _music;
    private MusicPlayer _instance;
    private void Awake()
    {
        _music = GetComponent<AudioSource>();

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _music.playOnAwake = false;
        _music.spatialBlend = 0;
        _music.Play();
    }

    public void SetVolume(float volume)
    {
        _music.volume = Mathf.Clamp( volume, 0f, 1f);
    }
}
