using System.Linq;
using UnityEngine;

public class WinCell
{
    public int[] WinCombination { get; private set; }

    public WinCell(int[] winCombination) => WinCombination = winCombination;

    public bool CheckWin(Cell[] cells, Signs sign)
    {
        var check = WinCombination.Count(win => cells[win].Sign == sign);
        return check == WinCombination.Length;
    }

    public void ShowMass()
    {
        var combination = WinCombination.Aggregate(string.Empty, (current, win) => current + win);
        Debug.Log(combination);
    }
}