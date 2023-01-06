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
        switch (_matchSettings)
        {
            case MatchSettings.PLvsPC:
                _firstPlayer = new HumanPlayer(pens.PlayerPen, Signs.X, grid);
                _secondPlayer = new AIPlayer(pens.BluePen, Signs.O, grid);
                _firstPlayer.OnDoStep += _secondPlayer.DoStep;
                _currentPlayer = _firstPlayer;
                break;
            case MatchSettings.PLvsPL:
                break;
            case MatchSettings.PCvsPC:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    // private void TryDoStep(Cell cell)
    // {
    //     if (_currentPlayer.IsPlaying || _currentPlayer is AIPlayer) return;
    //     _currentPlayer.DoStep(cell);
    // }
    //
    // private void TryDoAIStep()
    // {
    //     if (_currentPlayer.IsPlaying || _currentPlayer is HumanPlayer) return;
    //     var cell = _grids.CurrentGrid.Cells.FirstOrDefault(cell => cell.Sign == null);
    //     if (cell == default) return;
    //     _currentPlayer.DoStep(_grids.CurrentGrid.Cells.FirstOrDefault(cell => cell.Sign == null));
    // }

    private void SwitchPlayer() => _currentPlayer = _currentPlayer == _firstPlayer ? _secondPlayer : _firstPlayer;

    private void Start()
    {
        _grids.Enable3X3();
    }
}

public enum MatchSettings
{
    PLvsPC,
    PLvsPL,
    PCvsPC
}