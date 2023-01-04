using System;
using Tools;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    public event Action<Signs> OnSetSign;
    public Signs? Sign { get; private set; }

    [SerializeField] private Line _x;
    [SerializeField] private Line _o;

    public void OnPointerDown(PointerEventData eventData) => SetSign(Signs.X, Toolbox.Get<Pens>().PlayerPen);

    public void SetSign(Signs sign, Pen pen)
    {
        if (Sign != null) return;
        Sign = sign;
        switch (sign)
        {
            case Signs.X:
                _x.DoAnimation(pen);
                break;
            case Signs.O:
                _o.DoAnimation(pen);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(sign), sign, null);
        }

        OnSetSign?.Invoke(sign);
    }

    private void Awake()
    {
        _x.SetParameters();
        _x.ClearLines();
        _o.SetParameters();
        _o.ClearLines();
    }
}

public enum Signs
{
    X,
    O
}