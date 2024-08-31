using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Core.GameFsm
{
    public class LevelGameState : GameStateBase
    {
        public override void Enter()
        {
            if (!Application.isMobilePlatform)
                Cursor.lockState = CursorLockMode.Locked;
        }

        public override void Exit()
        {
        }
    }
}