using System.Collections.Generic;
using System.Threading.Tasks;

public interface IMovementService
{
    Task MovePieceAlong(Piece piece, List<Node> path);
}