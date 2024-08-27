using Muchachos.TideTurner.Runtime.Boot.Initializers;
using Muchachos.TideTurner.Runtime.Configuration;
using Muchachos.TideTurner.Runtime.Core;
using Muchachos.TideTurner.Runtime.Core.GameFsm;
using Muchachos.TideTurner.Runtime.Core.Input;
using Muchachos.TideTurner.Runtime.Core.SceneLoading;
using Muchachos.TideTurner.Runtime.Mobile;
using Muchachos.TideTurner.Runtime.UI;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ConfigProvider _configProvider;

        [SerializeField] private AudioMixer _mixer;

        [SerializeField] private ButtonSoundPlayer _soundPlayer;
        
        [SerializeField] private GlobalSceneLoader _globalSceneLoader;
        [SerializeField] private BlackScreen _blackScreen;
        
        public override void InstallBindings()
        {
            BindConfigProvider();
            BindSettingData();

            BindAudioMixerProvider();
            BindButtonSoundPlayer();

            BindSceneLoader();

            BindStateFactory();
            BindGameStateMachine();

            BindProjectInitializer();

            BindMobileInitializer();
            
            Container.Bind<YandexGamesIntegration>().AsSingle();

            Container.BindInstance(_globalSceneLoader).AsSingle();
            Container.BindInstance(_blackScreen).AsSingle();
        }

        private void BindMobileInitializer()
        {
            if (Application.isMobilePlatform)
                Container
                    .BindInterfacesTo<MobileInitializer>()
                    .AsSingle();
        }

        private void BindSceneLoader()
        {
            Container
                .Bind<ISceneLoader>()
                .To<SceneLoader>()
                .AsSingle();
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
                .Bind<IGameStateFactory>()
                .To<GameStateFactory>()
                .AsSingle();
        }

        private void BindGameStateMachine()
        {
            Container
                .BindInterfacesAndSelfTo<GameStateMachine>()
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