using Muchachos.TideTurner.Runtime.Configuration;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Slider _mouseX;

        [SerializeField] private Slider _musicVolume;
        [SerializeField] private Slider _soundVolume;
        
        [PropertySpace(SpaceBefore = 10)]
        [HideReferencePicker]
        [ShowInInspector]
        [EnableInPlayMode]
        public SettingsData Settings { get; private set; }
        
        private ISettingsController _controller;

        [Inject]
        public void Construct(SettingsData settings)
        {
            Settings = settings;
        }

        public void SetController(ISettingsController controller)
        {
            _controller = controller;
            
            _mouseX.onValueChanged.AddListener(UpdateMouseX);
            _musicVolume.onValueChanged.AddListener(UpdateMusicVolume);
            _soundVolume.onValueChanged.AddListener(UpdateSoundVolume);
        }

        public void SetMouseX(float value)
        {
            _mouseX.value = value;
        }

        public void SetMusicVolume(float value)
        {
            _musicVolume.value = value;
        }

        public void SetSoundVolume(float value)
        {
            _soundVolume.value = value;
        }

        private void UpdateMouseX(float value)
        {
            _controller.UpdateMouseX(value);
        }

        private void UpdateMusicVolume(float value)
        {
            _controller.UpdateMusicVolume(value);
        }

        private void UpdateSoundVolume(float value)
        {
            _controller.UpdateSoundVolume(value);
        }
    }
}