using System.Collections.Generic;

public class LevelDataText
{
    public int PieceCount;
    public int PointCount;
    public List<(float x, float y)> Points = new();
    public List<int> StartPositions = new();
    public List<int> TargetPositions = new();
    public int EdgeCount;
    public List<(int a, int b)> Edges = new();
}