using System.Linq;
using UnityEngine;

public class Pens : ManagerBase
{
    public Pen BluePen => _bluePens.First(pen => pen.IsFree);
    public Pen RedPen => _redPens.First(pen => pen.IsFree);
    [SerializeField] private Pen[] _redPens;
    [SerializeField] private Pen[] _bluePens;
}