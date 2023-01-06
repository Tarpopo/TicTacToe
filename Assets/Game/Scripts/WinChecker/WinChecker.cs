using Tools;
using UnityEngine;

public class WinChecker : ManagerBase
{
    [SerializeField] private int _gridsSizes;

    // [SerializeField] private Cell[] _cells;
    private WinCombination _combination;
    private Grids _grids;

    private void Start()
    {
        _grids = Toolbox.Get<Grids>();
        GenerateWinMassive(_gridsSizes);
        // foreach (var cell in _cells)
        // {
        //     cell.OnSetSign += CheckWin;
        // }
    }

    private void CheckWin(Signs sign)
    {
        if (_combination.CheckWin(_grids.CurrentGrid.Cells, sign))
        {
            print("Win");
        }
    }

    private void GenerateWinMassive(int gridsSizes)
    {
        _combination = new WinCombination(gridsSizes);
        // _combination.ShowAll();
    }
}