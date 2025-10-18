using System.Collections.Generic;
using UnityEngine;

public class UnityHighlightService : IHighlightService
{
    private readonly List<NodeView> _highlightedNodes = new();
    private PieceView _highlightedPiece;
    private readonly GameConfig _config;
    
    public UnityHighlightService(GameConfig config)
    {
        _config = config;
    }

    public void HighlightNodes(IEnumerable<Node> nodes)
    {
        foreach (var n in _highlightedNodes)
            if (n != null) 
                n.SetHighlighted(false, _config.NodeHighlightColor);
        
        _highlightedNodes.Clear();

        foreach (var node in nodes)
        {
            if (node.View != null)
            {
                node.View.SetHighlighted(true, _config.NodeHighlightColor);
                _highlightedNodes.Add(node.View);
            }
        }
    }

    public void HighlightPiece(Piece piece)
    {
        _highlightedPiece?.SetHighlighted(false);

        if (piece.View != null)
        {
            piece.View.SetHighlighted(true);
            _highlightedPiece = piece.View;
        }
    }

    public void ClearAll()
    {
        foreach (var n in _highlightedNodes)
            if (n != null)
                n.SetHighlighted(false, _config.NodeHighlightColor);
        
        _highlightedNodes.Clear();
    }

    public void ClearPiece()
    {
        _highlightedPiece?.SetHighlighted(false);
        _highlightedPiece = null;
    }
}