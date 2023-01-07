using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    public event Action<Cell> OnDown;
    public event Action<Signs> OnSetSign;
    public Signs? Sign { get; private set; }

    [SerializeField] private Line _x;
    [SerializeField] private Line _o;

    public void OnPointerDown(PointerEventData eventData) => OnDown?.Invoke(this);

    public void ClearCell()
    {
        Sign = null;
        ClearLines();
    }

    public void SetSign(Signs sign, Pen pen, Action onSignSet)
    {
        if (Sign != null) return;
        Sign = sign;
        switch (sign)
        {
            case Signs.X:
                _x.DoAnimation(pen, onSignSet);
                break;
            case Signs.O:
                _o.DoAnimation(pen, onSignSet);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sign), sign, null);
        }

        OnSetSign?.Invoke(sign);
    }

    private void Awake()
    {
        _x.SetParameters();
        _o.SetParameters();
    }

    private void ClearLines()
    {
        _x.ClearLines();
        _o.ClearLines();
    }
}

public enum Signs
{
    X,
    O
}