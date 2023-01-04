using UnityEngine;
using UnityEngine.Events;

public class SelectButton : BaseButton
{
    public bool IsSelect { get; private set; }
    [SerializeField] private UnityEvent _onSelect;
    [SerializeField] private Line _selectLine;

    private void Awake()
    {
        _selectLine.SetParameters(GetComponentsInChildren<LineRenderer>());
        _selectLine.ClearLines();
    }

    public void Select(Pen pen)
    {
        if (IsSelect) return;
        IsSelect = true;
        DoSelectAnimation(pen);
    }

    public void Deselect()
    {
        IsSelect = false;
        _selectLine.ClearLines();
    }

    private void DoSelectAnimation(Pen pen) => _selectLine.DoAnimation(pen, () => _onSelect?.Invoke());
}