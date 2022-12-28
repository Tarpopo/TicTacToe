using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Line _x;
    [SerializeField] private Line _o;
    private Signs? _sign;

    public void OnPointerDown(PointerEventData eventData)
    {
        SetSign(Signs.X);
    }

    public void SetSign(Signs sign)
    {
        if (_sign != null) return;
        _sign = sign;
        if (sign == Signs.X) _x.DoAnimation();
        if (sign == Signs.O) _o.DoAnimation();
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