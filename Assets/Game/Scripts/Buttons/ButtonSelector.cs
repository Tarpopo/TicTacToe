using Tools;
using UnityEngine;

public class ButtonSelector : BaseButtonSelector
{
    [SerializeField] private SelectButton _selectedButton;

    protected override void Start() => _selectedButton.Select(Toolbox.Get<Pens>().BluePen);

    protected override void OnDisable()
    {
    }

    protected override void TrySelectButton(BaseButton button)
    {
        var select = (SelectButton)button;
        if (select.IsSelect) return;
        DeselectAll();
        select.Select(Toolbox.Get<Pens>().PlayerPen);
    }
}