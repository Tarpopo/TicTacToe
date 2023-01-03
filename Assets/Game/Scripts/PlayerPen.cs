using System;
using Tools;
using UnityEngine;

public class PlayerPen : Pen
{
    private Camera _camera;
    private Pen _playerPen;

    public override void ActivePen(Vector3 position, Action onActive)
    {
        IsFree = false;
        onActive?.Invoke();
    }

    public override void DeactivatePen() => IsFree = true;

    private void Start()
    {
        _playerPen = Toolbox.Get<Pens>().PlayerPen;
        _camera = Camera.main;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (_playerPen.IsFree == false) return;
        var position = _camera.ScreenToWorldPoint(Input.mousePosition);
        position.z = _playerPen.Transform.position.z;
        _playerPen.Transform.position = position;
    }
}