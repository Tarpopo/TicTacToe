using System.Collections.Generic;
using System.Linq;

public struct WinCombination
{
    private readonly List<WinCell> _winCells;
    public int GridSize { get; private set; }

    public WinCombination(int gridSize)
    {
        GridSize = gridSize;
        _winCells = new List<WinCell>(GetWinElementsSize(GridSize));
        AddHorizontalCombination();
        AddVerticalCombination();
        AddRightDiagonal();
        AddLeftDiagonal();
    }

    public bool CheckWin(Cell[] cells, Signs sign, out WinCell winCell)
    {
        winCell = _winCells.FirstOrDefault(item => item.CheckWin(cells, sign));
        return winCell != default;
    }

    public void ShowAll() => _winCells.ForEach(win => win.ShowMass());

    private void AddCombination(WinCell win) => _winCells.Add(win);
    private static int GetWinElementsSize(int gridSize) => gridSize * 2 + 2;


    private void AddLeftDiagonal()
    {
        var mass = new int[GridSize];
        mass[0] = 0;
        var previous = mass[0];
        for (int i = 1; i < GridSize; i++)
        {
            mass[i] = previous + GridSize + 1;
            previous = mass[i];
        }

        AddCombination(new WinCell(mass));
    }

    private void AddRightDiagonal()
    {
        var mass = new int[GridSize];
        mass[0] = GridSize - 1;
        var previous = mass[0];
        for (int i = 1; i < GridSize; i++)
        {
            mass[i] = previous + GridSize - 1;
            previous = mass[i];
        }

        AddCombination(new WinCell(mass));
    }

    private void AddHorizontalCombination()
    {
        var previous = 0;

        for (int j = 0; j < GridSize; j++)
        {
            var mass = new int[GridSize];
            for (int k = 0; k < GridSize; k++)
            {
                mass[k] = previous;
                previous++;
            }

            AddCombination(new WinCell(mass));
        }
    }

    private void AddVerticalCombination()
    {
        var previous = 0;

        for (int j = 0; j < GridSize; j++)
        {
            var mass = new int[GridSize];

            for (int k = 0; k < GridSize; k++)
            {
                if (k == 0)
                {
                    previous = k + j;
                    mass[k] = previous;
                }

                else
                {
                    previous += GridSize;
                    mass[k] = previous;
                }
            }

            AddCombination(new WinCell(mass));
        }
    }
}