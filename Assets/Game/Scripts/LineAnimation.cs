using DG.Tweening;
using Tools;
using UnityEngine;

public class LineAnimation : MonoBehaviour
{
    [SerializeField] private PenType _penType = PenType.Blue;
    [SerializeField] private Line _line;
    [SerializeField] private bool _doOnEnable;
    [SerializeField] private bool _doOnStart;

    private void Awake()
    {
        _line.SetParameters();
    }

    private void OnEnable()
    {
        if (_doOnEnable) DoAnimation();
    }

    private void Start()
    {
        if (_doOnStart) DoAnimation();
    }

    public void DoAnimation()
    {
        _line.ClearLines();
        _line.DoAnimation(Toolbox.Get<Pens>().GetPen(_penType));
    }
}