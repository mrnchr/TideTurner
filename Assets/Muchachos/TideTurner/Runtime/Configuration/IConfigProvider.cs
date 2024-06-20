using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Configuration
{
    public interface IConfigProvider
    {
        TConfig Get<TConfig>() where TConfig : ScriptableObject;
    }
}