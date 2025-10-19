using System.Collections.Generic;
using UnityEngine;

public class BoardBuilder : MonoBehaviour
{
    private GameConfig _gameConfig;
    private NodeView _nodePrefab;
    private PieceView _piecePrefab;
    
    private readonly List<GameObject> _instances = new();

    public void Build(Board board)
    {
        _nodePrefab = Resources.Load<NodeView>(AssetsPath.NodePrefab);
        _piecePrefab = Resources.Load<PieceView>(AssetsPath.PiecePrefab);
        
        Clear();
        
        _gameConfig = ServiceContainer.Resolve<GameConfig>();
        foreach (var node in board.Nodes.Values)
        {
            Vector2 worldPos = node.Pos * _gameConfig.NodeScale * _gameConfig.Spacing;
            node.WorldPos = worldPos;

            var obj = Instantiate(_nodePrefab, worldPos, Quaternion.identity, transform);
            node.View = obj.GetComponent<NodeView>();
            node.View.Model = node;
            obj.name = "Node_" + node.Id;
            
            _instances.Add(obj.gameObject);
        }

        foreach (var piece in board.Pieces)
        {
            var obj = Instantiate(_piecePrefab, piece.CurrentNode.WorldPos, Quaternion.identity, transform);
            piece.View = obj.GetComponent<PieceView>();
            piece.View.Model = piece;

            piece.View.SetColor(ColorPalette.GetColorByIndex(piece.ColorId));

            obj.name = "Piece_" + piece.Id;
            
            _instances.Add(obj.gameObject);
        }

        DrawEdges(board);
    }

    private void DrawEdges(Board board)
    {
        foreach (var node in board.Nodes.Values)
        {
            foreach (var n in node.Neighbors)
            {
                if (n <= node.Id) 
                    continue;
                
                var other = board.GetNode(n);
                
                if (other == null)
                    continue;

                var go = new GameObject($"Edge_{node.Id}_{other.Id}");
                go.transform.parent = transform;
                var lr = go.AddComponent<LineRenderer>();
                lr.positionCount = _gameConfig.LinePositionCount;
                lr.widthMultiplier = _gameConfig.LineWidth;
                lr.material = _gameConfig.LineMaterial;
                lr.sortingOrder = -1;
                lr.SetPosition(0, node.WorldPos);
                lr.SetPosition(1, other.WorldPos);
                
                _instances.Add(go);

            }
        }
    }
    
    private void Clear()
    {
        foreach (var go in _instances)
            if (go != null)
                Destroy(go);

        _instances.Clear();
    }
}