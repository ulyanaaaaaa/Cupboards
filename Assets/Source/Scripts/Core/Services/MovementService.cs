using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class MovementService : IMovementService
{
    private readonly GameConfig _config;
    
    public MovementService()
    {
        _config = ServiceContainer.Resolve<GameConfig>();
    }
    
    public async Task MovePieceAlong(Piece piece, List<Node> path)
    {
        foreach (var node in path)
        {
            Vector3 targetPos = node.WorldPos;
            float distance = Vector3.Distance(piece.View.transform.position, targetPos);
            float duration = distance * _config.DurationPerUnit; 

            var tcs = new TaskCompletionSource<bool>();
            piece.View.transform.DOMove(targetPos, duration)
                .SetEase(Ease.InOutSine)
                .OnComplete(() => tcs.SetResult(true));

            await tcs.Task;
        }
    }
}