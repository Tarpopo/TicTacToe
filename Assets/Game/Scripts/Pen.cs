using System;
using UnityEngine;

[Serializable]
public class Pen : MonoBehaviour
{
    public bool IsFree => gameObject.activeSelf == false;
    public Transform Transform => transform;
    public Gradient PenGradient => _gradient;
    [SerializeField] private Gradient _gradient;

    public void ActivePen()
    {
        gameObject.SetActive(true);
    }

    public void DeactivatePen()
    {
        gameObject.SetActive(false);
    }
}