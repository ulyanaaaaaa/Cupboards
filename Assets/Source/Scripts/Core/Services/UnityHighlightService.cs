using System.Collections.Generic;

public class UnityHighlightService : IHighlightService
{
    private readonly List<NodeView> _highlightedNodes = new();
    private readonly GameConfig _config;
    private PieceView _highlightedPiece;
    
    public UnityHighlightService(GameConfig config)
    {
        _config = config;
    }

    public void HighlightNodes(IEnumerable<Node> nodes)
    {
        foreach (var n in _highlightedNodes)
            n.SetHighlighted(false, _config.NodeHighlightColor);
        
        _highlightedNodes.Clear();

        foreach (var node in nodes)
        {
            node.View.SetHighlighted(true, _config.NodeHighlightColor);
            _highlightedNodes.Add(node.View);
        }
    }

    public void HighlightPiece(Piece piece)
    {
        _highlightedPiece?.SetHighlighted(false);
        piece.View.SetHighlighted(true);
        _highlightedPiece = piece.View;
    }

    public void ClearAll()
    {
        foreach (var n in _highlightedNodes)
            n.SetHighlighted(false, _config.NodeHighlightColor);
        
        _highlightedNodes.Clear();
    }

    public void ClearPiece()
    {
        _highlightedPiece?.SetHighlighted(false);
        _highlightedPiece = null;
    }
}