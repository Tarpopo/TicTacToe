using System;
using System.Collections;
using UnityEngine;

public class LinePainter : ManagerBase
{
    public void Draw(Pen pen, LineData[] linesData, float duration, Action onEnd = null)
    {
        pen.ActivePen();
        foreach (var lineData in linesData) lineData.SetGradient(pen.PenGradient);
        StartCoroutine(PenDrawCoroutine(pen.Transform, linesData, duration, () =>
        {
            onEnd?.Invoke();
            pen.DeactivatePen();
        }));
    }

    private void SetLinePosition(LineRenderer line, Vector3 position, int index)
    {
        line.positionCount = index + 1;
        line.SetPosition(index, position);
    }

    private IEnumerator PenDrawCoroutine(Transform pen, LineData[] linesData, float duration, Action onEnd)
    {
        foreach (var lineData in linesData)
        {
            yield return StartCoroutine(DoPathMove(pen, lineData.Transform, duration, lineData.Points,
                (position, index) => SetLinePosition(lineData.LineRenderer, position, index)));
        }

        onEnd?.Invoke();
    }

    private IEnumerator DoPathMove(Transform moving, Transform lineTransform, float duration, Vector3[] points,
        Action<Vector3, int> onPointMoveEnd)
    {
        for (int i = 0; i < points.Length; i++)
        {
            yield return StartCoroutine(DoMove(moving, duration, moving.position,
                lineTransform.TransformPoint(points[i]),
                () => onPointMoveEnd?.Invoke(points[i], i)));
        }
    }

    private static IEnumerator DoMove(Transform moving, float duration, Vector3 from, Vector3 to, Action onEnd)
    {
        for (var time = 0f; time <= 1f; time += Time.deltaTime / duration)
        {
            moving.position = Vector3.Lerp(from, to, time);
            yield return null;
        }

        onEnd?.Invoke();
    }
}