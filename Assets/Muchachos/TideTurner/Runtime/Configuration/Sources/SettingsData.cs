using System;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Configuration
{
    [Serializable]
    public class SettingsData
    {
        public float MouseSensitivity;

        [Range(0.0001f, 1)]
        public float MusicVolume;

        [Range(0.0001f, 1)]
        public float SoundVolume;

        public void CopyFrom(SettingsData from)
        {
            MouseSensitivity = from.MouseSensitivity;
            MusicVolume = from.MusicVolume;
            SoundVolume = from.SoundVolume;
        }
    }
}