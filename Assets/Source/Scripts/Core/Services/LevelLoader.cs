using System;
using UnityEngine;

public class LevelLoader
{
    public LevelDataText LoadLevel(string resourcePath)
    {
        var ta = Resources.Load<TextAsset>(resourcePath);
        var lines = ta.text.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        int idx = 0;
        
        var data = new LevelDataText();

        data.PieceCount = int.Parse(lines[idx++].Trim());

        data.PointCount = int.Parse(lines[idx++].Trim());

        for (int i = 0; i < data.PointCount; i++)
        {
            var parts = lines[idx++].Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);
            data.Points.Add((float.Parse(parts[0]), float.Parse(parts[1])));
        }

        var startParts = lines[idx++].Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var s in startParts)
            data.StartPositions.Add(int.Parse(s));

        var targetParts = lines[idx++].Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);
        foreach (var s in targetParts)
            data.TargetPositions.Add(int.Parse(s));

        data.EdgeCount = int.Parse(lines[idx++].Trim());

        for (int i = 0; i < data.EdgeCount; i++)
        {
            var e = lines[idx++].Trim().Split(',', StringSplitOptions.RemoveEmptyEntries);
            data.Edges.Add((int.Parse(e[0]), int.Parse(e[1])));
        }

        return data;
    }
}
