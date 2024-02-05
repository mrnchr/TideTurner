using UnityEngine;

public class SceneContext : MonoBehaviour
{
    private ProjectContext _ctx;
    private LevelBootstrap _bootstrap;
    private SettingsController _settings;
    private GameStateMachine _gameMachine;

    private void Awake()
    {
        _ctx = FindAnyObjectByType<ProjectContext>();
        if (!_ctx)
            _ctx = Instantiate(Resources.Load<ProjectContext>("Core"));

        _bootstrap = FindAnyObjectByType<LevelBootstrap>();
        if (_bootstrap)
            _bootstrap.Construct();

        _settings = FindAnyObjectByType<SettingsController>(FindObjectsInactive.Include);
        if (_settings)
            _settings.Construct();

        _gameMachine = FindAnyObjectByType<GameStateMachine>();
    }

    private void Start()
    {
        if(!_ctx.IsInitialized)
            _ctx.Init();
        
        if (_gameMachine)
        {
            if(_bootstrap)
                _gameMachine.ChangeState<LevelGameState>();
            else 
                _gameMachine.ChangeState<MenuGameState>();
        }
        
        if(_bootstrap)
            _bootstrap.Init();
        
        if(_settings)
            _settings.Init();
    }
}