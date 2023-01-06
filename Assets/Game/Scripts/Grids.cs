using System;
using UnityEngine;

public class Grids : ManagerBase
{
    public Grid CurrentGrid => _grids[_currentGrid];
    public Grid[] AllGrids => _grids;
    [SerializeField] private Grid[] _grids;
    private int _currentGrid;

    public void EnableGrid(GridType gridType)
    {
        foreach (var grid in _grids) grid.SetActive(false);
        _currentGrid = (int)gridType;
        CurrentGrid.SetActive(true);
    }

    public void Enable3X3() => EnableGrid(GridType.ThreeXThree);
    public void Enable5X5() => EnableGrid(GridType.FiveXFive);
}

[Serializable]
public class Grid
{
    public Cell[] Cells => _cells;
    [SerializeField] private GameObject _grid;
    [SerializeField] private Cell[] _cells;
    public void SetActive(bool active) => _grid.SetActive(active);
}

public enum GridType
{
    ThreeXThree,
    FiveXFive,
}