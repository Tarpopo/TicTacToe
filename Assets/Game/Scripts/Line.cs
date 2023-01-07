using System;
using UnityEngine;

[Serializable]
public class Line
{
    [SerializeField] private LineRenderer[] _lineRenderers;
    [SerializeField] private float _duration;
    private LineData[] _linesData;

    public void SetParameters(LineRenderer[] lineRenderers)
    {
        _linesData = new LineData[lineRenderers.Length];
        for (int i = 0; i < lineRenderers.Length; i++) _linesData[i] = new LineData(lineRenderers[i]);
    }

    public void SetParameters(Vector3[] points)
    {
        _linesData = new LineData[1];
        _linesData[0] = new LineData(_lineRenderers[0], points);
    }

    public void SetParameters()
    {
        _linesData = new LineData[_lineRenderers.Length];
        for (int i = 0; i < _lineRenderers.Length; i++) _linesData[i] = new LineData(_lineRenderers[i]);
    }

    public void ClearLines()
    {
        foreach (var line in _linesData) line.LineRenderer.positionCount = 0;
    }

    public void DoAnimation(Pen pen, Action onEnd = null) => pen.DoAnimation(_linesData, _duration, onEnd);
}

[Serializable]
public class LineData
{
    public Vector3[] Points { get; }
    public Transform Transform => LineRenderer.transform;
    public LineRenderer LineRenderer { get; private set; }

    public LineData(LineRenderer lineRenderer)
    {
        LineRenderer = lineRenderer;
        Points = new Vector3[LineRenderer.positionCount];
        LineRenderer.GetPositions(Points);
    }

    public LineData(LineRenderer lineRenderer, Vector3[] points)
    {
        LineRenderer = lineRenderer;
        Points = new Vector3[points.Length];
        Points = points;
    }

    public void SetGradient(Gradient gradient) => LineRenderer.colorGradient = gradient;
}