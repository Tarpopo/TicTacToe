using UnityEngine;

public record AIPlayer : BasePlayer
{
    public AIPlayer(Pen pen, Signs sign,Grid grid) : base(pen, sign, grid)
    {
    }
}