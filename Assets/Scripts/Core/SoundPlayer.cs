using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private bool isSound3D;
    [SerializeField] private bool playOnAwake = false;

    public bool IsPlaying => _sound.isPlaying;
    public float SoundLength=> _sound.clip.length;



    private AudioSource _sound;

    private void Awake()
    {
        _sound = GetComponent<AudioSource>();

        _sound.playOnAwake = playOnAwake;
        _sound.spatialBlend = 1;        
    }

    public void SetVolume(float volume)
    {
        _sound.volume = Mathf.Clamp(volume, 0f, 1f);
    }

    public void SetSoundState(bool playSound)
    {
        if (playSound == true)
            _sound.Play();
        else if (playSound == false)
            _sound.Stop();
    }
}
