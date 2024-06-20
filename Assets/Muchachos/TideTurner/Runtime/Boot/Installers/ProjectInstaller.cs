using Muchachos.TideTurner.Runtime.Boot.Initializers;
using Muchachos.TideTurner.Runtime.Configuration;
using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Core.GameFsm;
using Muchachos.TideTurner.Runtime.UI;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField]
        private ConfigProvider _configProvider;

        [SerializeField]
        private AudioMixer _mixer;

        [SerializeField]
        private ButtonSoundPlayer _soundPlayer;

        public override void InstallBindings()
        {
            BindConfigProvider();
            BindSettingData();

            BindAudioMixerProvider();
            BindButtonSoundPlayer();

            BindStateFactory();
            BindGameStateMachine();

            BindProjectInitializer();
        }

        private void BindButtonSoundPlayer()
        {
            Container
                .Bind<IButtonSoundPlayer>()
                .FromInstance(_soundPlayer)
                .AsSingle();
        }

        private void BindStateFactory()
        {
            Container
                .Bind<IStateFactory>()
                .To<StateFactory>()
                .AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container
                .Bind<GameStateMachine>()
                .AsSingle();
        }

        private void BindAudioMixerProvider()
        {
            Container
                .Bind<IAudioMixerProvider>()
                .To<AudioMixerProvider>()
                .AsSingle()
                .WithArguments(_mixer);
        }

        private void BindProjectInitializer()
        {
            Container
                .Bind<IInitializable>()
                .To<ProjectInitializer>()
                .AsSingle();
        }

        private void BindSettingData()
        {
            Container
                .Bind<SettingsData>()
                .AsSingle();
        }

        private void BindConfigProvider()
        {
            Container
                .Bind<IConfigProvider>()
                .FromInstance(_configProvider)
                .AsSingle();
        }
    }
}