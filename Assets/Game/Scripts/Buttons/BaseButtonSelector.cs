using System;
using System.Linq;
using UnityEngine;

public abstract class BaseButtonSelector : MonoBehaviour
{
    private SelectButton[] _buttons;

    private void Awake()
    {
        _buttons = GetComponentsInChildren<SelectButton>().ToArray();
        foreach (var button in _buttons) button.OnButtonDown += TrySelectButton;
    }

    protected virtual void Start()
    {
    }

    protected virtual void OnDisable() => DeselectAll();

    private void OnDestroy()
    {
        foreach (var button in _buttons) button.OnButtonDown -= TrySelectButton;
    }

    protected void DeselectAll()
    {
        foreach (var selectButton in _buttons) selectButton.Deselect();
    }

    protected abstract void TrySelectButton(BaseButton button);
}