﻿using Muchachos.TideTurner.Runtime.Core;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class LoseWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;

        [SerializeField] private SoundPlayer deathSound;

        public void SetActive(bool value)
        {
            _window.SetActive(value);
            deathSound.SetSoundState(value ? SoundState.Play : SoundState.Stop);
        }
    }
}