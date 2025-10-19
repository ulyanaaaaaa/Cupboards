using UnityEngine;

public class WinState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly WinView _view;

    public WinState(GameStateMachine stateMachine, WinView view)
    {
        _stateMachine = stateMachine;
        _view = view;
    }

    public void Enter()
    {
        _view.Show();
        _view.SetRestartAction(OnRestart);
    }

    private void OnRestart()
    {
        _view.Hide();
        _stateMachine.Enter<LoadLevelState>();
    }

    public void Exit() { }
    public void Tick() { }
}