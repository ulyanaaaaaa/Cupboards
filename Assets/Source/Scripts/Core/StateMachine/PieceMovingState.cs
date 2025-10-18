using System.Collections.Generic;
using UnityEngine;

public class PieceMovingState : IState
{
    private readonly IMovementService _movement;
    private readonly GameStateMachine _stateMachine;
    private Piece _piece;
    private List<Node> _path;

    public PieceMovingState(GameStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
        _movement = ServiceContainer.Resolve<IMovementService>();
    }

    public void Setup(Piece piece, List<Node> path)
    {
        _piece = piece;
        _path = path;
    }

    public async void Enter()
    {
        await _movement.MovePieceAlong(_piece, _path);
        _piece.CurrentNode.OccupiedBy = null;
        var last = _path[^1];
        _piece.CurrentNode = last;
        last.OccupiedBy = _piece;
        _stateMachine.Enter<PlayerTurnState>();
    }

    public void Exit()
    {
        _piece = null; 
        _path = null;
    }
    public void Tick() { }
}