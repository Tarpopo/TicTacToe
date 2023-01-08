using System;
using Tools;
using UnityEngine;

public class MatchMaker : ManagerBase
{
    [SerializeField] private Grids _grids;
    private MatchSettings _matchSettings = MatchSettings.PLvsPC;
    private BasePlayer _firstPlayer;
    private BasePlayer _secondPlayer;
    private BasePlayer _currentPlayer;
    private Signs _humanSign = Signs.X;
    private bool _isHumanFirst = true;
    public void SetPLvsPC() => _matchSettings = MatchSettings.PLvsPC;
    public void SetPLvsPL() => _matchSettings = MatchSettings.PLvsPL;
    public void SetPCvsPC() => _matchSettings = MatchSettings.PCvsPC;

    public void SetHumanStep(bool isFirst) => _isHumanFirst = isFirst;

    public void EndMatch()
    {
        var grids = Toolbox.Get<Grids>().CurrentGrid;
        UnSubscribeFromCells();
        grids.ClearCells();
        MatchStates.SetMatchEnded();
    }

    public void SetMainSign(int signIndex) => _humanSign = (Signs)signIndex;

    public void CreateMatch()
    {
        var pens = Toolbox.Get<Pens>();
        var grids = Toolbox.Get<Grids>().CurrentGrid;
        UnSubscribeFromCells();
        grids.ClearCells();
        MatchStates.SetMatchFree();
        switch (_matchSettings)
        {
            case MatchSettings.PLvsPC:
                if (_isHumanFirst)
                {
                    _firstPlayer = new HumanPlayer(pens.PlayerPen, _humanSign, grids);
                    _secondPlayer = new AIPlayer(pens.BluePen, _humanSign.GetOtherSign(), grids);
                    _firstPlayer.OnDoStep += DoStep;
                }
                else
                {
                    _firstPlayer = new AIPlayer(pens.BluePen, _humanSign.GetOtherSign(), grids);
                    _secondPlayer = new HumanPlayer(pens.PlayerPen, _humanSign, grids);
                    _secondPlayer.OnDoStep += DoStep;
                }

                SubscribeToCells();
                break;
            case MatchSettings.PLvsPL:
                _firstPlayer = new HumanPlayer(pens.PlayerPen, _humanSign, grids);
                _secondPlayer = new HumanPlayer(pens.PlayerPen, _humanSign.GetOtherSign(), grids);
                SubscribeToCells();
                break;
            case MatchSettings.PCvsPC:
                _firstPlayer = new AIPlayer(pens.RedPen, Signs.X, grids);
                _secondPlayer = new AIPlayer(pens.BluePen, Signs.O, grids);
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
        if (_currentPlayer.IsFree == false || MatchStates.Free == false) return;
        _currentPlayer.DoStep(cell, SwitchPlayer);
    }

    private void DoStep()
    {
        if (_currentPlayer.IsFree == false || MatchStates.Free == false) return;
        _currentPlayer.DoStep(SwitchPlayer);
    }

    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer.Equals(_firstPlayer) ? _secondPlayer : _firstPlayer;
    }
}

public enum MatchSettings
{
    PLvsPC,
    PLvsPL,
    PCvsPC
}