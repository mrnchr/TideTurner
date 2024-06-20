using Muchachos.TideTurner.Runtime.UI;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField]
        private SettingsView _settingsView;
        
        public override void InstallBindings()
        {
            BindSettingsController();
        }

        private void BindSettingsController()
        {
            Container
                .BindInterfacesTo<SettingsController>()
                .AsSingle()
                .WithArguments(_settingsView);
        }
    }
}