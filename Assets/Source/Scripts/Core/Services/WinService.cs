using System.Collections.Generic;

public class WinService
{
    private LevelDataText _levelData;
    private List<Piece> _pieces;
    private GameStateMachine _stateMachine;

    public void Initialize(LevelDataText data, List<Piece> pieces, GameStateMachine stateMachine)
    {
        _levelData = data;
        _pieces = pieces;
        _stateMachine = stateMachine;
    }
    
    public bool CheckWin()
    {
        if (_levelData == null || _pieces == null)
            return false;

        for (int i = 0; i < _levelData.TargetPositions.Count; i++)
        {
            if (i >= _pieces.Count)
                return false;

            if (_pieces[i].CurrentNode.Id != _levelData.TargetPositions[i])
            {
                return false;
            }
        }

        _stateMachine.Enter<WinState>();
        return true;
    }
}