﻿using Muchachos.TideTurner.Runtime.Core;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class WinWindow : MonoBehaviour
    {
        [SerializeField] private GameObject _window;
        [SerializeField] private SoundPlayer soundPlayer;
        
        public void SetActive(bool value)
        {
            _window.SetActive(value);
            soundPlayer.SetSoundState(value ? SoundState.Play : SoundState.Stop);
        }
    }
}