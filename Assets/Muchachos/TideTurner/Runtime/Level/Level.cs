using System.Collections;
using Muchachos.TideTurner.Runtime.Core.SceneLoading;
using Muchachos.TideTurner.Runtime.Level.FloatingObjects;
using Muchachos.TideTurner.Runtime.Level.LevelFsm;
using Muchachos.TideTurner.Runtime.Level.Obstacles.Cannon;
using Muchachos.TideTurner.Runtime.Level.Savings;
using Muchachos.TideTurner.Runtime.Mobile;
using UnityEngine;
using Zenject;

namespace Muchachos.TideTurner.Runtime.Level
{
    public class Level : MonoBehaviour
    {
        [SerializeField] private float deathDelay = 1f;

        private LevelStateMachine _levelMachine;
        private AbstractMoonData _moonData;
        private AbstractMoon _moon;
        private Boat _boat;
        private Water _water;
        private Cannon[] _cannons;
        private CameraMovement _cameraMovement;
        private Coroutine _coroutine;
        private ISceneLoader _sceneLoader;
        private CheckPointHandler _handler;
    
        [Inject]
        public void Construct(ISceneLoader sceneLoader, LevelStateMachine levelMachine)
        {
            _sceneLoader = sceneLoader;
            _levelMachine = levelMachine;
        }

        public void Construct(AbstractMoonData moonData,
            AbstractMoon abstractMoon,
            Boat boat,
            Water water,
            Cannon[] cannons,
            CameraMovement cameraMovement,
            CheckPointHandler handler)
        {
            _moonData = moonData;
            _moon = abstractMoon;
            _boat = boat;
            _water = water;
            _cannons = cannons;
            _cameraMovement = cameraMovement;
            _handler = handler;
        }

        public void Init()
        {
            _moonData.Init();
            _moon.Init();
            _water.Init();
            _boat.Init();
            _cameraMovement.Init();

            foreach (Cannon cannon in _cannons)
                cannon.Init();

            _handler.Init();
        }

        public void Reborn()
        {
            CheckPoint last = _handler.GetLastCheck();
        
            _moonData.Init();
            _moon.Init();
            _boat.SetPosition(last.transform.position);
            _boat.ResetLogic();
            _water.Movement.SetWaterLevel(last.transform.position);
            _cameraMovement.Init();
        
            foreach (Cannon cannon in _cannons)
                cannon.Init();
        }

        public void CallReborn()
        {
            if (!_handler.GetLastCheck())
                _levelMachine.ChangeState<RestartLevelState>();
            else
                _levelMachine.ChangeState<RebornLevelState>();
        }

        public void ToMenu()
        {
            _levelMachine.ChangeState<StayLevelState>();
            _sceneLoader.LoadScene(SceneType.Menu);
        }

        public void Lose()
        {
            if (IsLose() || _levelMachine.CurrentState is WinLevelState)
                return;

            _boat.SetLoseState();
            _coroutine = StartCoroutine(StartDeathTimer());
        }

        public bool IsLose() => _levelMachine.CurrentState is LoseLevelState || _coroutine != null;

        private IEnumerator StartDeathTimer()
        {
            yield return new WaitForSeconds(deathDelay);

            _levelMachine.ChangeState<LoseLevelState>();

            _coroutine = null;
        }

        public void Win()
        {
            if (_levelMachine.CurrentState is WinLevelState || IsLose())
                return;

            _levelMachine.ChangeState<WinLevelState>();
        }
    }
}