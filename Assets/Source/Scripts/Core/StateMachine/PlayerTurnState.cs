public class PlayerTurnState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly IInputService _input;
    private readonly IHighlightService _highlight;
    private Piece _selected;

    public PlayerTurnState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _input = ServiceContainer.Resolve<IInputService>();
        _highlight = ServiceContainer.Resolve<IHighlightService>();
    }

    public void Enter() { }

    public void Exit()
    {
        _highlight.ClearAll();
        _selected = null;
    }
    
    public void Tick()
    {
        if (!_input.GetPointerDown()) 
            return;
        
        var go = _input.GetRaycastObject();
        if (go == null)
        {
            Deselect();
            return;
        }

        if (go.TryGetComponent(out PieceView pieceView))
        {
            Select(pieceView.Model);
            return;
        }

        if (go.TryGetComponent(out NodeView nodeView) && _selected != null)
        {
            var path = PathFinder.FindPath(_selected.CurrentNode, 
                nodeView.Model, 
                node => node.OccupiedBy != null && node.OccupiedBy != _selected);
            
            if (path == null) 
                return;
            
            var move = _stateMachine.Get<PieceMovingState>();
            move.Setup(_selected, path);
            _stateMachine.Enter<PieceMovingState>();
        }
    }

    private void Select(Piece piece)
    {
        _selected = piece;
        _highlight.HighlightPiece(piece);
        
        var reachable = PathFinder.ReachableNodes(
            piece.CurrentNode, 
                n => n.OccupiedBy != null && n.OccupiedBy != piece);
        _highlight.HighlightNodes(reachable);
    }

    private void Deselect()
    {
        _highlight.ClearAll();  
        _highlight.ClearPiece(); 
        _selected = null;
    }
}