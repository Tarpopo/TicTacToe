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

    public void DoStep(Cell cell, Action onEnd)
    {
        cell.SetSign(_sign, _playerPen, () =>
        {
            onEnd?.Invoke();
            OnDoStep?.Invoke();
        });
    }

    public virtual void DoStep(Action onEnd)
    {
        
    }
}