using System;
using System.Collections.Generic;
using Muchachos.TideTurner.Runtime.Core;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Configuration
{
    [CreateAssetMenu(menuName = CAC.PREFAB_PROVIDER_MENU, fileName = CAC.PREFAB_PROVIDER_FILE)]
    public class PrefabProvider : ScriptableObject
    {
        public List<PrefabTuple> Prefabs;

        public GameObject Get(EntityType id)
        {
            return Prefabs.Find(x => x.Id == id).Prefab;
        }
    }

    [Serializable]
    public struct PrefabTuple
    {
        public EntityType Id;
        public GameObject Prefab;
    }
}