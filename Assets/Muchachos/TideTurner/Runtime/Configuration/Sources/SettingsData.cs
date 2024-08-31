using System;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Configuration
{
    [Serializable]
    public class SettingsData
    {
        public float MouseSensitivity;

        [Range(Idents.AudioParameters.MinVolume, Idents.AudioParameters.MaxVolume)]
        public float MusicVolume;

        [Range(Idents.AudioParameters.MinVolume, Idents.AudioParameters.MaxVolume)]
        public float SoundVolume;

        public void CopyFrom(SettingsData from)
        {
            MouseSensitivity = from.MouseSensitivity;
            MusicVolume = from.MusicVolume;
            SoundVolume = from.SoundVolume;
        }
    }
}