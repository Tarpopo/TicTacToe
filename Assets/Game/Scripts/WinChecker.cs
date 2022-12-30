using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WinChecker : MonoBehaviour
{
    [SerializeField] private int _gridsSizes;
    [SerializeField] private Cell[] _cells;
    private WinCombination _combination;

    private void Start()
    {
        GenerateWinMassive(_gridsSizes);
        foreach (var cell in _cells)
        {
            cell.OnSetSign += CheckWin;
        }
    }

    private void CheckWin(Signs sign)
    {
        if (_combination.CheckWin(_cells, sign))
        {
            print("You win");
        }
    }

    private void GenerateWinMassive(int gridsSizes)
    {
        _combination = new WinCombination(gridsSizes);
        _combination.ShowAll();
    }
}

public readonly struct WinCell
{
    private readonly int[] _winCombination;

    public WinCell(int[] winCombination) => _winCombination = winCombination;

    public bool CheckWin(Cell[] cells, Signs sign)
    {
        var check = _winCombination.Count(win => cells[win].Sign == sign);
        return check == _winCombination.Length;
    }

    public void ShowMass()
    {
        var combination = _winCombination.Aggregate(string.Empty, (current, win) => current + win);
        Debug.Log(combination);
    }
}

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
    }

    public bool CheckWin(Cell[] cells, Signs sign) => _winCells.Any(winCell => winCell.CheckWin(cells, sign));

    public void ShowAll() => _winCells.ForEach(win => win.ShowMass());

    private void AddCombination(WinCell win) => _winCells.Add(win);
    private static int GetWinElementsSize(int gridSize) => gridSize * 2 + 2;

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