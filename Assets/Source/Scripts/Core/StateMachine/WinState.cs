public class WinState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly WinView _view;
    private readonly LevelManager _levelManager;

    public WinState(GameStateMachine stateMachine, WinView view)
    {
        _stateMachine = stateMachine;
        _view = view;
        _levelManager = ServiceContainer.Resolve<LevelManager>();
    }

    public void Enter()
    {
        _view.Show();
        _view.SetNextAction(LoadNextLevel);
    }

    private void LoadNextLevel()
    {
        _levelManager.NextLevel();
        _stateMachine.Enter<LoadLevelState>();
        _view.Hide();
    }

    public void Exit() { }

    public void Tick() { }
}