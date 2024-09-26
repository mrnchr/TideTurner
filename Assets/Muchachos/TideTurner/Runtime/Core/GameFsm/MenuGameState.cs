using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Core.GameFsm
{
    public class MenuGameState : GameStateBase
    {
        public override void Enter()
        {
            if (!Application.isMobilePlatform)
                Cursor.lockState = CursorLockMode.Confined;
        }

        public override void Exit()
        {
        }
    }
}