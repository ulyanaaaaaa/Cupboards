using UnityEngine;

public static class ColorPalette
{
    private static GameConfig _config;

    public static void Initialize(GameConfig config)
    {
        _config = config;
    }
    
    public static Color GetColorByIndex(int idx)
        => _config.PieceColors[idx % _config.PieceColors.Count];
}