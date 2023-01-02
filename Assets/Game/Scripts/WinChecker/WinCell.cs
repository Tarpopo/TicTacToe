using System.Linq;
using UnityEngine;

public struct WinCell
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