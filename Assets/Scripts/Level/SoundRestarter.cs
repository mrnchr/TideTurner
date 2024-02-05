using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundRestarter : MonoBehaviour
{
    private SoundPlayer[] _sounds;
    private List<SoundPlayer> _played;

    public void FindAll()   
    {
        _sounds = FindObjectsByType<SoundPlayer>(FindObjectsInactive.Include, FindObjectsSortMode.None);
    }

    public void CachePlayedSound()
    {
        FindAll();
        _played = _sounds.Where(x => x.IsPlaying).ToList();
    }

    public void SetSoundAllPlayed(SoundState state)
    {
        foreach (SoundPlayer sound in _played)
            sound.SetSoundState(state);
    }
}