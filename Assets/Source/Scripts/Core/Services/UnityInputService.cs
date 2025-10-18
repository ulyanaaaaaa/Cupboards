using UnityEngine;

public class UnityInputService : IInputService
{
    private readonly Camera _camera;
    private readonly LayerMask _pieceLayer;
    private readonly LayerMask _nodeLayer;

    public UnityInputService(Camera camera)
    {
        _camera = camera;
        _pieceLayer = LayerMask.GetMask(AssetsPath.PiecesLayer);
        _nodeLayer = LayerMask.GetMask(AssetsPath.NodesLayer);
    }

    public bool GetPointerDown() => Input.GetMouseButtonDown(0);

    public Vector2 PointerWorldPosition()
    {
        var mp = Input.mousePosition;
        var world = _camera.ScreenToWorldPoint(mp);
        return new Vector2(world.x, world.y);
    }

    public GameObject GetRaycastObject()
    {
        var pos = PointerWorldPosition();

        var pieceHit = Physics2D.OverlapPoint(pos, _pieceLayer);
        if (pieceHit != null) 
            return pieceHit.gameObject;

        var nodeHit = Physics2D.OverlapPoint(pos, _nodeLayer);
        if (nodeHit != null) 
            return nodeHit.gameObject;

        return null;
    }
}