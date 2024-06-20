using Muchachos.TideTurner.Runtime.Core;
using UnityEngine;

namespace Muchachos.TideTurner.Runtime.UI
{
    public class MenuBootstrap : Bootstrap
    {
        public override void Construct()
        {
            var buttons = FindObjectsByType<ButtonSoundCaller>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        
            foreach (ButtonSoundCaller button in buttons)
                button.Construct();
        }

        public override void Init()
        {
        }
    }
}