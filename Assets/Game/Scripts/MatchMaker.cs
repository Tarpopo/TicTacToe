using System;
using Tools;
using UnityEngine;

public class MatchMaker : MonoBehaviour
{
    [SerializeField] private Grids _grids;
    private MatchSettings _matchSettings = MatchSettings.PLvsPC;
    private BasePlayer _firstPlayer;
    private BasePlayer _secondPlayer;
    private BasePlayer _currentPlayer;

    public void SetPLvsPC() => _matchSettings = MatchSettings.PLvsPC;
    public void SetPLvsPL() => _matchSettings = MatchSettings.PLvsPL;
    public void SetPCvsPC() => _matchSettings = MatchSettings.PCvsPC;

    public void CreateMatch()
    {
        var pens = Toolbox.Get<Pens>();
        var grid = Toolbox.Get<Grids>().CurrentGrid;
        UnSubscribeFromCells();

        switch (_matchSettings)
        {
            case MatchSettings.PLvsPC:
                _firstPlayer = new HumanPlayer(pens.PlayerPen, Signs.X, grid);
                _secondPlayer = new AIPlayer(pens.BluePen, Signs.O, grid);
                _firstPlayer.OnDoStep += DoStep;
                SubscribeToCells();
                break;
            case MatchSettings.PLvsPL:
                _firstPlayer = new HumanPlayer(pens.PlayerPen, Signs.X, grid);
                _secondPlayer = new HumanPlayer(pens.PlayerPen, Signs.O, grid);
                SubscribeToCells();
                break;
            case MatchSettings.PCvsPC:
                _firstPlayer = new AIPlayer(pens.RedPen, Signs.X, grid);
                _secondPlayer = new AIPlayer(pens.BluePen, Signs.O, grid);
                _firstPlayer.OnDoStep += DoStep;
                _secondPlayer.OnDoStep += DoStep;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _currentPlayer = _firstPlayer;
        if (_currentPlayer is AIPlayer) DoStep();
    }

    private void Start() => _grids.Enable3X3();

    private void SubscribeToCells()
    {
        foreach (var cell in _grids.CurrentGrid.Cells) cell.OnDown += DoStep;
    }

    private void UnSubscribeFromCells()
    {
        foreach (var cell in _grids.CurrentGrid.Cells) cell.OnDown -= DoStep;
    }

    private void DoStep(Cell cell)
    {
        _currentPlayer.DoStep(cell);
        SwitchPlayer();
    }

    private void DoStep()
    {
        _currentPlayer.DoStep();
        SwitchPlayer();
    }

    private void SwitchPlayer() => _currentPlayer = _currentPlayer.Equals(_firstPlayer) ? _secondPlayer : _firstPlayer;
}

public enum MatchSettings
{
    PLvsPC,
    PLvsPL,
    PCvsPC
}