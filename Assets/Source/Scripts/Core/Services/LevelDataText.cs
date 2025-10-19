using System.Collections.Generic;

public class LevelDataText
{
    public int PieceCount;
    public int PointCount;
    public readonly List<(float x, float y)> Points = new();
    public readonly List<int> StartPositions = new();
    public readonly List<int> TargetPositions = new();
    public int EdgeCount;
    public readonly List<(int a, int b)> Edges = new();
}