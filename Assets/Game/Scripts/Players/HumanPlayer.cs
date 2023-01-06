public record HumanPlayer : BasePlayer
{
    public HumanPlayer(Pen pen, Signs sign, Grid grid) : base(pen, sign, grid)
    {
        foreach (var cell in grid.Cells)
        {
            cell.OnDown += DoStep;
        }
    }

    ~HumanPlayer()
    {
        foreach (var cell in _grid.Cells)
        {
            cell.OnDown -= DoStep;
        }
    }
}