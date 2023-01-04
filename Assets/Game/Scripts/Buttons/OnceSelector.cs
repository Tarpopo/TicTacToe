using Tools;

public class OnceSelector : BaseButtonSelector
{
    private bool _isButtonSelected;

    protected override void OnDisable()
    {
        base.OnDisable();
        _isButtonSelected = false;
    }

    protected override void TrySelectButton(BaseButton button)
    {
        if (_isButtonSelected) return;
        _isButtonSelected = true;
        var select = (SelectButton)button;
        select.Select(Toolbox.Get<Pens>().PlayerPen);
    }
}