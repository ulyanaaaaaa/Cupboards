using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Levels")]
    public List<TextAsset> Levels = new List<TextAsset>();

    [Header("Board Settings")]
    public float NodeScale;
    public float Spacing;

    [Header("Mini Map Settings")]
    public float MiniScale;
    public Vector2 MiniMapOffset;

    [Header("Highlight Colors")]
    public Color NodeHighlightColor;

    [Header("Piece Animation")]
    public float PulseDuration;
    public float PulseScaleFactor;
    public float PulseScaleDuration;

    [Header("Line Settings")]
    public float LineWidth;
    public int LinePositionCount;
    public Material LineMaterial;
    
    [Header("Movement")]
    public float DurationPerUnit;

    [Header("Piece Colors")] [Header("Piece Colors")]
    public List<Color> PieceColors = new List<Color>();
}