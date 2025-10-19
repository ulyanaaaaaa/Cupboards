using System.Collections.Generic;
using UnityEngine;

public class LevelManager
{
    private readonly List<TextAsset> _levels;
    private int _currentLevelIndex;

    public LevelManager(GameConfig gameConfig)
    {
        _levels = gameConfig.Levels != null ? new List<TextAsset>(gameConfig.Levels) : new List<TextAsset>();
        _currentLevelIndex = 0;
    }

    public TextAsset GetCurrentLevel() =>
        _levels[_currentLevelIndex];
    
    public void NextLevel()
    {
        _currentLevelIndex = (_currentLevelIndex + 1) % _levels.Count;
    }
}