using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Configuration
{
    [CreateAssetMenu(menuName = CAC.SETTINGS_MENU, fileName = CAC.SETTINGS_NAME)]
    public class SettingsConfig : ScriptableObject
    {
        public SettingsData DefaultSettings;
    }
}