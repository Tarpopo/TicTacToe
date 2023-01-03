using System;
using UnityEngine;

public class ButtonSelector : MonoBehaviour
{
    [SerializeField] private BaseButton[] _buttons;
    private bool _isButtonSelected;

    private void Awake()
    {
        foreach (var button in _buttons) button.OnButtonDown += TrySelectButton;
    }

    private void OnDestroy()
    {
        foreach (var button in _buttons) button.OnButtonDown -= TrySelectButton;
    }

    private void TrySelectButton(BaseButton button)
    {
        if (_isButtonSelected) return;
        _isButtonSelected = true;
        var select = (SelectButton)button;
        select.DoSelectAnimation();
    }
}