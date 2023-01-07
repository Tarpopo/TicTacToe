using System;
using Tools;
using UnityEngine;

[Serializable]
public class Pen : MonoBehaviour
{
    public bool IsFree { get; protected set; } = true;

    public Transform Transform => transform;
    public Gradient PenGradient => _gradient;

    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _showDuration = 0.005f;
    [SerializeField] private float _hideDuration = 0.005f;

    public virtual void ActivePen(Vector3 position, Action onActive)
    {
        gameObject.SetActive(true);
        IsFree = false;
        StartCoroutine(LinePainter.DoMove(Transform, _showDuration, position, onActive));
    }

    public virtual void DeactivatePen(Action onDeactive)
    {
        StartCoroutine(LinePainter.DoMove(Transform, _hideDuration, Vector3.one * 15, () =>
        {
            gameObject.SetActive(false);
            IsFree = true;
            onDeactive?.Invoke();
        }));
    }

    public void DoAnimation(LineData[] lineData, float duration, Action onEnd = null)
    {
        IsFree = false;
        Toolbox.Get<LinePainter>().Draw(this, lineData, duration, () =>
        {
            IsFree = true;
            onEnd?.Invoke();
        });
    }
}