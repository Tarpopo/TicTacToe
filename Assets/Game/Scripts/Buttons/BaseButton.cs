using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseButton : MonoBehaviour, IPointerDownHandler
{
    public event Action<BaseButton> OnButtonDown;
    private bool _isActive;

    public void OnPointerDown(PointerEventData eventData) => OnDown();

    public virtual void OnDown() => OnButtonDown?.Invoke(this);
}