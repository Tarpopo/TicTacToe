using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    public event Action<Signs> OnSetSign;
    public Signs? Sign { get; private set; }

    [SerializeField] private Line _x;
    [SerializeField] private Line _o;

    public void OnPointerDown(PointerEventData eventData)
    {
        SetSign(Signs.O);
    }

    public void SetSign(Signs sign)
    {
        if (Sign != null) return;
        Sign = sign;
        switch (sign)
        {
            case Signs.X:
                _x.DoAnimation();
                break;
            case Signs.O:
                _o.DoAnimation();
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