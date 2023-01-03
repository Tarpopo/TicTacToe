using System;
using UnityEngine;

[Serializable]
public class Pen : MonoBehaviour
{
    public bool IsFree { get; protected set; } = true;

    public Transform Transform => transform;
    public Gradient PenGradient => _gradient;

    [SerializeField] private Gradient _gradient;
    [SerializeField] private float _showHideDuration = 0.2f;

    public virtual void ActivePen(Vector3 position, Action onActive)
    {
        gameObject.SetActive(true);
        IsFree = false;
        StartCoroutine(LinePainter.DoMove(Transform, _showHideDuration, Transform.position, position, onActive));
    }

    public virtual void DeactivatePen()
    {
        StartCoroutine(LinePainter.DoMove(Transform, _showHideDuration, Transform.position, Vector3.one * 20,
            () =>
            {
                gameObject.SetActive(false);
                IsFree = true;
            }));
    }
}