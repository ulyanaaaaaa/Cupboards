using UnityEngine;

public class GameEntryPoint : MonoBehaviour
{
    [SerializeField] private BoardBuilder _boardBuilder;
    [SerializeField] private TargetBoardView _targetBoardView;
    [SerializeField] private WinView _winView;
    private GameStateMachine _stateMachine;

    private void Awake()
    {
        _stateMachine = new GameStateMachine();
        var bootstrap = new BootstrapState(_stateMachine, _boardBuilder, _targetBoardView, _winView);
        _stateMachine.Register(bootstrap);
        _stateMachine.Enter<BootstrapState>();
    }

    private void Update() => _stateMachine.Tick();
}