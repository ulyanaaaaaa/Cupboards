using UnityEngine;
using System.Collections.Generic;

public class LoadLevelState : IState
{
    private readonly GameStateMachine _stateMachine;
    private readonly BoardBuilder _boardBuilder;
    private readonly TargetBoardView _targetView;

    public LoadLevelState(GameStateMachine stateMachine, BoardBuilder boardBuilder, TargetBoardView targetView)
    {
        _stateMachine = stateMachine;
        _boardBuilder = boardBuilder;
        _targetView = targetView;
    } 

    public void Enter()
    {
        var loader = new LevelLoader();
        var data = loader.LoadLevel(AssetsPath.LevelPath1); 
        var board = new Board();

        for (int i = 0; i < data.PointCount; i++)
        {
            var p = data.Points[i];
            var node = new Node
            {
                Id = i + 1,
                Pos = new Vector2(p.x, p.y),
                Neighbors = new List<int>()
            };
            board.Nodes[node.Id] = node;
        }

        foreach (var edge in data.Edges)
        {
            var a = board.GetNode(edge.a);
            var b = board.GetNode(edge.b);
            
            if (a == null || b == null)
                continue;
            
            if (!a.Neighbors.Contains(b.Id)) 
                a.Neighbors.Add(b.Id);
            
            if (!b.Neighbors.Contains(a.Id)) 
                b.Neighbors.Add(a.Id);
        }

        for (int i = 0; i < data.PieceCount; i++)
        {
            int nodeId = data.StartPositions[i];
            var node = board.GetNode(nodeId);
            
            if (node == null) 
                continue;

            var piece = new Piece
            {
                Id = i + 1,
                CurrentNode = node,
                ColorId = i
            };

            node.OccupiedBy = piece;
            board.Pieces.Add(piece);
        }

        ServiceContainer.Register(board);
        _boardBuilder.Build(board);
        _targetView.BuildTarget(data);
        
        ServiceContainer.Resolve<WinService>().Initialize(data, board.Pieces, _stateMachine);

        _stateMachine.Enter<PlayerTurnState>();
    }

    public void Exit() { }
    public void Tick() { }
}
