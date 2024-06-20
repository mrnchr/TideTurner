using System;
using System.Collections.Generic;
using Muchachos.TideTurner.Runtime.Core.SceneLoading;
using TriInspector;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Configuration
{
    [CreateAssetMenu(menuName = CAC.SCENE_MENU, fileName = CAC.SCENE_NAME)]
    public class SceneConfig : ScriptableObject
    {
        [ListDrawerSettings(AlwaysExpanded = true)]
        public List<SceneTuple> Scenes;

        public string Get(SceneType id)
        {
            return Scenes.Find(x => x.Id == id).Scene;
        }
        
    }
    
    [Serializable]
    [DeclareHorizontalGroup(nameof(SceneTuple))]
    public struct SceneTuple
    {
        [GroupNext(nameof(SceneTuple))]
        [HideLabel]
        public SceneType Id;

        [Scene]
        [HideLabel]
        public string Scene;
    }
}