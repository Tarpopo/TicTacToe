using System;
using System.Linq;

public abstract record BasePlayer
{
    public bool IsFree => _playerPen.IsFree;
    public event Action OnDoStep;
    protected Pen _playerPen;
    protected Signs _sign;
    protected Grid _grid;

    protected BasePlayer(Pen pen, Signs sign, Grid grid)
    {
        _playerPen = pen;
        _sign = sign;
        _grid = grid;
    }

    public void DoStep(Cell cell)
    {
        MatchStates.SetPlayerDrawingState(this);
        cell.SetSign(_sign, _playerPen, () =>
        {
            MatchStates.DisablePlayerDrawingState(this);
            OnDoStep?.Invoke();
        });
    }

    public void DoStep()
    {
        var cell = _grid.Cells.FirstOrDefault(cell => cell.Sign == null);
        if (cell == default) return;
        DoStep(cell);
    }
}