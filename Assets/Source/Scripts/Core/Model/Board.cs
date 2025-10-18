using System.Collections.Generic;

public class Board
{
    public Dictionary<int, Node> Nodes = new();
    public List<Piece> Pieces = new();

    public Node GetNode(int id) => Nodes.TryGetValue(id, out var n) ? n : null;
}