using System;
using UnityEngine;

public class PlayerPen : Pen
{
    private Camera _camera;

    public override void ActivePen(Vector3 position, Action onActive)
    {
        IsFree = false;
        onActive?.Invoke();
    }

    public override void DeactivatePen(Action onDeactive)
    {
        IsFree = true;
        onDeactive?.Invoke();
    }

    private void Start()
    {
        _camera = Camera.main;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (IsFree == false) return;
        var position = _camera.ScreenToWorldPoint(Input.mousePosition);
        position.z = Transform.position.z;
        Transform.position = position;
    }
}