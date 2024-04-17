using UnityEngine;

public class LevelFreezer : MonoBehaviour
{
    private LevelUpdater _updater;
    private InputController _input;
    private SoundRestarter _sound;

    public void Construct(LevelUpdater updater, InputController input, SoundRestarter sound)
    {
        _updater = updater;
        _input = input;
        _sound = sound;
    }

    public void Freeze()
    {
        if (Application.isMobilePlatform == false) 
            Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        _input.SetPause(true);
        _updater.SetPause(true);
        _sound.CachePlayedSound();
        _sound.SetSoundAllPlayed(SoundState.Pause);
    }

    public void Unfreeze()
    {
        Time.timeScale = 1;
        _input.SetPause(false);
        _updater.SetPause(false);
        _sound.SetSoundAllPlayed(SoundState.Play);
    }
}