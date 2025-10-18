using UnityEngine;
using System.Collections.Generic;

public class TargetBoardView : MonoBehaviour
{
    [SerializeField] private GameObject NodePrefab;
    [SerializeField] private GameObject PiecePrefab;
    private readonly List<GameObject> _instances = new();
    private GameConfig _config;
    
    public void BuildTarget(LevelDataText data)
    {
        _config = ServiceContainer.Resolve<GameConfig>();
        
        Clear();
        var nodes = new Dictionary<int, Vector2>();

        for (int i = 0; i < data.PointCount; i++)
        {
            var p = data.Points[i];
            Vector2 miniPos = new Vector2(p.x, p.y) * _config.MiniScale * _config.Spacing + _config.MiniMapOffset;
            nodes[i + 1] = miniPos;

            var nodeObj = Instantiate(NodePrefab, miniPos, Quaternion.identity, transform);
            nodeObj.transform.localScale = Vector3.one * _config.MiniScale;
            _instances.Add(nodeObj);
        }

        foreach (var edge in data.Edges)
        {
            if (!nodes.ContainsKey(edge.a) || !nodes.ContainsKey(edge.b)) continue;

            var posA = nodes[edge.a];
            var posB = nodes[edge.b];

            var go = new GameObject($"Edge_{edge.a}_{edge.b}");
            go.transform.parent = transform;

            var lr = go.AddComponent<LineRenderer>();
            lr.positionCount = _config.LinePositionCount;
            lr.widthMultiplier = _config.LineWidth;
            lr.material = _config.LineMaterial;
            lr.SetPosition(0, posA);
            lr.SetPosition(1, posB);

            _instances.Add(go);
        }

        for (int i = 0; i < data.TargetPositions.Count; i++)
        {
            int nodeId = data.TargetPositions[i];
            if (!nodes.ContainsKey(nodeId)) continue;

            var miniPos = nodes[nodeId];
            var pieceObj = Instantiate(PiecePrefab, miniPos, Quaternion.identity, transform);
            pieceObj.transform.localScale = Vector3.one * _config.MiniScale;

            var pv = pieceObj.GetComponent<PieceView>();
            pv.SetColor(ColorPalette.GetColorByIndex(i));

            _instances.Add(pieceObj);
        }
    }

    private void Clear()
    {
        foreach (var o in _instances)
            if (o != null)
                Destroy(o);
        
        _instances.Clear();
    }
}
