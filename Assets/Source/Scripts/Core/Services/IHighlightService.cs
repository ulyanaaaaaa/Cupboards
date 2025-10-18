using System.Collections.Generic;

public interface IHighlightService
{
    void HighlightNodes(IEnumerable<Node> nodes);
    void HighlightPiece(Piece piece);
    void ClearPiece();
    void ClearAll();
}