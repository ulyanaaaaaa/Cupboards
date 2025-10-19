using UnityEngine;

public class BootstrapState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly BoardBuilder _boardBuilder;
    private readonly TargetBoardView _targetBoardView;
    private readonly WinView _winView;

    public BootstrapState(GameStateMachine stateMachine,
        BoardBuilder boardBuilder,
        TargetBoardView targetBoardView,
        WinView winView)
    {
        _stateMachine = stateMachine;
        _boardBuilder = boardBuilder;
        _targetBoardView = targetBoardView;
        _winView = winView;
    }

    public void Enter()
    {
        RegisterServices();
        RegisterStates();
        _stateMachine.Enter<LoadLevelState>();
    }

    public void Exit() { }
    public void Tick() { }

    private void RegisterServices()
    {
        var config = Resources.Load<GameConfig>(AssetsPath.GameConfigPath);
        ServiceContainer.Register(config);
        ServiceContainer.Register<IInputService>(new UnityInputService(Camera.main));
        ServiceContainer.Register<IMovementService>(new MovementService());
        ServiceContainer.Register<IHighlightService>(new UnityHighlightService(config));
        ServiceContainer.Register(new WinService());

        if (!ServiceContainer.TryResolve<LevelManager>(out _))
            ServiceContainer.Register(new LevelManager(config));

        ColorPalette.Initialize(config);
    }

    private void RegisterStates()
    {
        _stateMachine.Register(new LoadLevelState(_stateMachine, _boardBuilder, _targetBoardView));
        _stateMachine.Register(new PlayerTurnState(_stateMachine));
        _stateMachine.Register(new PieceMovingState(_stateMachine));
        _stateMachine.Register(new WinState(_stateMachine, _winView)); 
    }
}