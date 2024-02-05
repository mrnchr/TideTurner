using UnityEngine;

public class LevelMusic : MonoBehaviour
{
    [SerializeField] private SoundPlayer _sound;

    public void SetSound(SoundState state)
    {
        _sound.SetSoundState(state);
    }
}