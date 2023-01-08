using System;
using System.Linq;

public record AIPlayer : BasePlayer
{
    public AIPlayer(Pen pen, Signs sign, Grid grid) : base(pen, sign, grid)
    {
    }

    public override void DoStep(Action onEnd)
    {
        var cell = _grid.Cells.FirstOrDefault(cell => cell.Sign == null);
        if (cell == default) return;
        cell = _grid.GridSize > 3
            ? _grid.RandomFreeCell
            : _grid.Cells[MoveCalculator.FindBestMove(_grid.Signs, _sign, GetOtherSign(_sign), _grid.GridSize)];
        DoStep(cell, onEnd);
    }

    private Signs GetOtherSign(Signs sign) => sign == Signs.O ? Signs.X : Signs.O;
}