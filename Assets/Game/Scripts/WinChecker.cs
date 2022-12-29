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
        _combination = new WinCombination();
        _combination.AddCombinations(gridsSizes);
        _combination.ShowAll();
    }
}

public class WinCombination
{
    private int[,] _winVertical;
    private int[,] _winHorizontal;
    private int[,] _diagonal;

    public bool CheckWin(Cell[] cells, Signs sign)
    {
        for (int i = 0; i < _winVertical.GetLength(0); i++)
        {
            var check = 0;
            for (int j = 0; j < _winVertical.GetLength(1); j++)
            {
                if (cells[_winVertical[i, j]].Sign == sign) check++;
                else
                {
                    check = 0;
                    break;
                }
            }

            if (check == _winVertical.GetLength(0))
            {
                return true;
            }
        }

        // for (int i = 0; i < _winHorizontal.GetLength(0); i++)
        // {
        //     for (int j = 0; j < _winHorizontal.GetLength(1); j++)
        //     {
        //     }
        // }

        return false;
    }

    public void AddVertical(int[,] vertical) => _winVertical = vertical;

    public void AddHorizontal(int[,] horizontal) => _winHorizontal = horizontal;

    public void AddDiagonals()
    {
    }

    public void ShowAll()
    {
        _winHorizontal.PrintMass();
        _winVertical.PrintMass();
    }
}

public static class WinCombinationsCalculator
{
    public static void PrintMass(this int[,] mass)
    {
        for (int i = 0; i < mass.GetLength(0); i++)
        {
            Debug.Log("//////////");
            for (int j = 0; j < mass.GetLength(1); j++)
            {
                Debug.Log(mass[i, j]);
            }
        }
    }

    public static void AddCombinations(this WinCombination win, int gridSize)
    {
        win.AddHorizontal(GetHorizontalCombinations(gridSize));
        win.AddVertical(GetVerticalCombinations(gridSize));
    }

    public static int[,] GetHorizontalCombinations(int gridSize)
    {
        var mass = new int[gridSize, gridSize];
        var previous = 0;

        for (int j = 0; j < gridSize; j++)
        {
            for (int k = 0; k < gridSize; k++)
            {
                mass[j, k] = previous;
                previous++;
            }
        }

        return mass;
    }

    public static int[,] GetVerticalCombinations(int gridSize)
    {
        var mass = new int[gridSize, gridSize];
        var previous = 0;

        for (int j = 0; j < gridSize; j++)
        {
            for (int k = 0; k < gridSize; k++)
            {
                if (k == 0)
                {
                    previous = k + j;
                    mass[j, k] = previous;
                }

                else
                {
                    previous += gridSize;
                    mass[j, k] = previous;
                }
            }
        }

        return mass;
    }

    private static int GetWinElementsSize(int gridSize) => gridSize * 2 + 2;
}