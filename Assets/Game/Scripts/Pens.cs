using System.Linq;
using UnityEngine;

public class Pens : ManagerBase
{
    public Pen BluePen => _bluePens.First(pen => pen.IsFree);
    public Pen RedPen => _redPens.First(pen => pen.IsFree);
    public Pen PlayerPen => _playerPen;
    [SerializeField] private Pen[] _redPens;
    [SerializeField] private Pen[] _bluePens;
    [SerializeField] private Pen _playerPen;

    public Pen GetPen(PenType penType) => penType == PenType.Blue ? BluePen : RedPen;
}

public enum PenType
{
    Blue,
    Red
}