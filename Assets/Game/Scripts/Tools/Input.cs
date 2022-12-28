using DefaultNamespace;
using UnityEngine;
using UnityEngine.EventSystems;

public class Input : ManagerBase, IAwake
{
    [SerializeField] private Physics2DRaycaster _raycaster;

    public void OnAwake()
    {
        DisableInput();
    }

    public void DisableInput() => _raycaster.enabled = false;

    public void EnableInput() => _raycaster.enabled = true;
}