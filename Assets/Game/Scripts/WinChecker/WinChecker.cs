using System.Linq;
using DefaultNamespace;
using Tools;
using UnityEngine;

public class WinChecker : ManagerBase, IStart
{
    [SerializeField] private Line _winLine;
    private Grids _grids;
    private Pens _pens;

    public void OnStart()
    {
        _grids = Toolbox.Get<Grids>();
        _pens = Toolbox.Get<Pens>();
        _winLine.SetParameters();
        foreach (var grid in _grids.AllGrids)
        {
            foreach (var cell in grid.Cells)
            {
                cell.OnSetSign += CheckWin;
            }
        }
    }

    public void Clear() => _winLine.ClearLines();

    private void CheckWin(Signs sign)
    {
        if (_grids.CurrentGrid.TryGetWinCombination(sign, out var cells) == false) return;
        _winLine.SetParameters(cells.Select(item => item.transform.position).ToArray());
        _winLine.DoAnimation(_pens.RedPen);
        MatchStates.SetMatchEnded();
    }
}