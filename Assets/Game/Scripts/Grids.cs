using System;
using UnityEngine;

public class Grids : MonoBehaviour
{
    public Grid CurrentGrid => _grids[_currentGrid];
    [SerializeField] private Grid[] _grids;
    private int _currentGrid;

    public void SetGrid(GridType grid) => _currentGrid = (int)grid;
}

[Serializable]
public class Grid
{
    public Cell[] Cells => _cells;
    [SerializeField] private Cell[] _cells;
}

public enum GridType
{
    ThreeXThree = 3,
    FiveXFive = 5,
}