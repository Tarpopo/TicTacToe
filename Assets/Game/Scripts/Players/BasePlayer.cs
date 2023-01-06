using System;
using System.Linq;

public abstract record BasePlayer
{
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

    public virtual void DoStep(Cell cell)
    {
        if (MatchStates.Free == false) return;
        MatchStates.SetPlayerDrawingState(this);
        cell.SetSign(_sign, _playerPen, () =>
        {
            MatchStates.DisablePlayerPlayingState(this);
            OnDoStep?.Invoke();
        });
    }

    public virtual void DoStep()
    {
        if (MatchStates.Free == false) return;
        var cell = _grid.Cells.FirstOrDefault(cell => cell.Sign == null);
        if (cell == default) return;
        DoStep(cell);
    }
}