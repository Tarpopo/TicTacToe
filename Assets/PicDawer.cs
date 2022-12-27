using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicDawer : MonoBehaviour
{
    [SerializeField] private Transform _pen;
    [SerializeField] private LineRenderer[] _lineRenderers;
    [SerializeField] private List<LinePicture> _lines;
    [SerializeField] private float _duration;

    private void Start()
    {
        foreach (var lineRenderer in _lineRenderers)
        {
            _lines.Add(new LinePicture(lineRenderer));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(DoPenMove());
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Draw());
        }
    }

    private IEnumerator Draw()
    {
        var length = _lines[0].Points.Length;
        _lines[0].LineRenderer.positionCount = 0;
        for (int i = 0; i < length; i++)
        {
            _lines[0].LineRenderer.positionCount = i + 1;
            _lines[0].LineRenderer.SetPosition(i, _lines[0].Points[i]);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator DoPenMove()
    {
        foreach (var line in _lines)
        {
            yield return StartCoroutine(DoPathMove(_pen, line.Transform, _duration, line.Points));
        }
    }

    private IEnumerator DoPathMove(Transform moving, Transform lineTransform, float duration, Vector3[] points)
    {
        foreach (var point in points)
        {
            yield return StartCoroutine(DoMove(moving, duration, moving.position, lineTransform.TransformPoint(point)));
        }
    }

    private IEnumerator DoMove(Transform moving, float duration, Vector3 from, Vector3 to)
    {
        for (float time = 0f; time <= 1f; time += Time.deltaTime / duration)
        {
            moving.position = Vector3.Lerp(from, to, time);
            yield return null;
        }
    }
}

[Serializable]
public class LinePicture
{
    public Vector3[] Points { get; }
    public Transform Transform => _lineRenderer.transform;
    public LineRenderer LineRenderer => _lineRenderer;
    [SerializeField] private LineRenderer _lineRenderer;

    public LinePicture(LineRenderer lineRenderer)
    {
        _lineRenderer = lineRenderer;
        Points = new Vector3[_lineRenderer.positionCount];
        _lineRenderer.GetPositions(Points);
        foreach (var point in Points)
        {
            Debug.Log(point);
        }
    }
}