using UnityEngine;

namespace Muchachos.TideTurner.Runtime.Level.LevelFsm
{
    public class StayLevelState : LevelStateBase
    {
        public override void Enter()
        {
            if (Application.isMobilePlatform == false)
                Cursor.lockState = CursorLockMode.Locked;
        }

        public override void Exit()
        {
        }
    }
}