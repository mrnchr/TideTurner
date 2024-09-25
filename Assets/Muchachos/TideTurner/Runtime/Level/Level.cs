using System;
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
        [SerializeField] private Cannon[] _cannons;

        public Cannon[] Cannons => _cannons;

        public event Action OnReborn;
        public event Action OnLose;

        private LevelStateMachine _levelMachine;
        private AbstractMoonData _moonData;
        private AbstractMoon _moon;
        private Boat _boat;
        private Water _water;
        private CameraMovement _cameraMovement;
        private Coroutine _coroutine;
        private ISceneLoader _sceneLoader;
        private CheckPointHandler _handler;
        private LevelFreezer _levelFreezer;
        private YandexGamesIntegration _yandexGamesIntegration;

        [Inject]
        public void Construct(ISceneLoader sceneLoader, 
            LevelStateMachine levelMachine,
            AbstractMoonData moonData,
            AbstractMoon abstractMoon,
            Boat boat,
            Water water,
            CameraMovement cameraMovement,
            CheckPointHandler handler,
            LevelFreezer levelFreezer,
            YandexGamesIntegration yaIntegration)
        {
            _sceneLoader = sceneLoader;
            _levelMachine = levelMachine;
            _levelFreezer = levelFreezer;

            _moonData = moonData;
            _moon = abstractMoon;
            _boat = boat;
            _water = water;
            _cameraMovement = cameraMovement;
            _handler = handler;

            _yandexGamesIntegration = yaIntegration;

            OnReborn += _yandexGamesIntegration.CallAdvWindow;
            OnReborn += _yandexGamesIntegration.CallRateGameWindow;

            _boat.OnLose += Lose;
        }

        public void Init()
        {
            Vector3 spawnPosition = _handler.GetSpawnPosition();

            _moonData.Init();
            _moon.Init();
            _water.Init();
            _boat.Init();
            _cameraMovement.Init();
            _water.Movement.SetWaterLevel(spawnPosition);
            
            foreach (Cannon cannon in _cannons)
                cannon.Init();
        }

        public void Reborn()
        {
            OnReborn?.Invoke();

            Vector3 spawnPosition = _handler.GetSpawnPosition();

            _moonData.Init();
            _moon.Init();
            _boat.SetPosition(spawnPosition);
            _boat.Reset();
            _water.Movement.SetWaterLevel(spawnPosition);
            _cameraMovement.Init();

            foreach (Cannon cannon in _cannons)
                cannon.Init();
        }

        public void CallReborn()
        {
            if (!_handler.WasCheckPoint())
                _levelMachine.ChangeState<RestartLevelState>();
            else
                _levelMachine.ChangeState<RebornLevelState>();
        }

        public void ToMenu()
        {
            _levelFreezer.Unfreeze();
            _sceneLoader.LoadScene(SceneType.Menu);
        }

        public void Lose()
        {
            if (IsLose() || _levelMachine.CurrentState is WinLevelState)
                return;

            _boat.SetLoseState();
            _coroutine = StartCoroutine(StartDeathTimer());

            OnLose?.Invoke();
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

        private void OnDisable()
        {
            OnReborn -= _yandexGamesIntegration.CallAdvWindow;
            OnReborn -= _yandexGamesIntegration.CallRateGameWindow;

            _boat.OnLose -= Lose;

            (_moon as MobileMoon)?.DisableCanvas();
        }
    }
}