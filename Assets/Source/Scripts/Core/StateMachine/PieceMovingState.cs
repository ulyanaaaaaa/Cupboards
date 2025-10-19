using System.Collections.Generic;
using UnityEngine;

public class PieceMovingState : IState
{
    private readonly IMovementService _movement;
    private readonly GameStateMachine _stateMachine;
    private Piece _piece;
    private List<Node> _path;
    private readonly IHighlightService _highlight;

    public PieceMovingState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _movement = ServiceContainer.Resolve<IMovementService>();
        _highlight = ServiceContainer.Resolve<IHighlightService>();
    }

    public void Setup(Piece piece, List<Node> path)
    {
        _piece = piece;
        _path = path;
    }
    
    public async void Enter()
    {
        var winService = ServiceContainer.Resolve<WinService>();
        await _movement.MovePieceAlong(_piece, _path);
    
        _piece.CurrentNode.OccupiedBy = null;
        var last = _path[^1];
        _piece.CurrentNode = last;
        last.OccupiedBy = _piece;

        last.OccupiedBy = _piece;
        
        _highlight.ClearPiece();

        if (winService.CheckWin())
        {
            _stateMachine.Enter<WinState>();
            return;
        }
        
        _stateMachine.Enter<PlayerTurnState>();
    }

    public void Exit()
    {
        _piece = null; 
        _path = null;
    }
    
    public void Tick() { }
}