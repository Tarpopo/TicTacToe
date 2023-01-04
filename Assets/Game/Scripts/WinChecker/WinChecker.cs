using UnityEngine;

public class WinChecker : MonoBehaviour
{
    [SerializeField] private int _gridsSizes;
    // [SerializeField] private Cell[] _cells;
    private WinCombination _combination;

    private void Start()
    {
        GenerateWinMassive(_gridsSizes);
        // foreach (var cell in _cells)
        // {
        //     cell.OnSetSign += CheckWin;
        // }
    }

    private void CheckWin(Signs sign)
    {
        // if (_combination.CheckWin(_cells, sign))
        // {
        //     print("You win");
        // }
    }

    private void GenerateWinMassive(int gridsSizes)
    {
        _combination = new WinCombination(gridsSizes);
        // _combination.ShowAll();
    }
}

