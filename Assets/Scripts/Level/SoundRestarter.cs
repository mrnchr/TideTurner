using UnityEngine;

public class SoundRestarter : MonoBehaviour
{
    private SoundPlayer[] _sounds;

    public void FindAll()
    {
        _sounds = FindObjectsByType<SoundPlayer>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

    public void SoundAll(SoundState state)
    {
        foreach (SoundPlayer sound in _sounds)
            sound.SetSoundState(state);
    }
}