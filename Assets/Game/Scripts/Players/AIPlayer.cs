using System;

public record AIPlayer : BasePlayer
{
    private int _stepsCount;

    public AIPlayer(Pen pen, Signs sign, Grid grid) : base(pen, sign, grid)
    {
    }

    public override void DoStep(Action onEnd)
    {
        if (_grid.CellsExist == false) return;
        DoStep(GetCell(_stepsCount), onEnd);
        _stepsCount++;
    }

    private Cell GetCell(int stepsCount)
    {
        if (_grid.TryGetMiddleCell(out var cell)) return cell;
        if (stepsCount <= _grid.GridSize / 2) return GetRandomCell();
        return _grid.GridSize <= 3 ? GetCalculatingCell() : GetRandomCell();
    }

    private Cell GetRandomCell() => _grid.RandomFreeCell;

    private Cell GetCalculatingCell() =>
        _grid.Cells[MoveCalculator.FindBestMove(_grid.Signs, _sign, _sign.GetOtherSign(), _grid.GridSize)];
}