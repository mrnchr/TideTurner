using Muchachos.TideTurner.Runtime.Configuration;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Boot.Initializers
{
    public class ProjectInitializer : IInitializable
    {
        private readonly SettingsData _settings;
        private readonly SettingsConfig _settingsConfig;

        public ProjectInitializer(SettingsData settings, IConfigProvider configProvider)
        {
            _settings = settings;
            _settingsConfig = configProvider.Get<SettingsConfig>();
        }
        
        public void Initialize()
        {
            _settings.CopyFrom(_settingsConfig.DefaultSettings);
        }
    }
}