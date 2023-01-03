using Tools;
using UnityEngine;
using UnityEngine.Events;

public class SelectButton : BaseButton
{
    [SerializeField] private UnityEvent _onSelect;
    [SerializeField] private Line _select;

    private void Awake()
    {
        _select.SetParameters();
        _select.ClearLines();
    }

    public void DoSelectAnimation() => _select.DoAnimation(Toolbox.Get<Pens>().PlayerPen, () => _onSelect?.Invoke());
}