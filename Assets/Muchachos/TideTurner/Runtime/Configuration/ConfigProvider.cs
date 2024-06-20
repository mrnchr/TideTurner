using System.Collections.Generic;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Configuration
{
    [CreateAssetMenu(menuName = CAC.CONFIG_PROVIDER_MENU, fileName = CAC.CONFIG_PROVIDER_NAME)]
    public class ConfigProvider : ScriptableObject, IConfigProvider
    {
        public List<ScriptableObject> Configs;

        public TConfig Get<TConfig>() where TConfig : ScriptableObject
        {
            return (TConfig)Configs.Find(x => x is TConfig);
        }
    }
}