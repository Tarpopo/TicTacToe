using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LinePainter : ManagerBase
{
    public void Draw(Pen pen, LineData[] linesData, float duration, Action onEnd = null)
    {
        pen.ActivePen(linesData[0].Transform.TransformPoint(linesData[0].Points[0]), () =>
        {
            foreach (var lineData in linesData) lineData.SetGradient(pen.PenGradient);
            StartCoroutine(PenDrawCoroutine(pen.Transform, linesData, duration, () =>
            {
                onEnd?.Invoke();
                pen.DeactivatePen();
            }));
        });
    }

    private static void SetLinePosition(LineRenderer line, Vector3 position, int index)
    {
        line.positionCount = index + 1;
        line.SetPosition(index, position);
    }

    private IEnumerator PenDrawCoroutine(Transform pen, LineData[] linesData, float duration, Action onEnd)
    {
        foreach (var lineData in linesData)
        {
            yield return StartCoroutine(DoMove(pen, duration * 20, pen.position,
                lineData.Transform.TransformPoint(lineData.Points[0])));
            yield return StartCoroutine(DoPathMove(pen, lineData.Transform, duration, lineData.Points,
                (position, index) => SetLinePosition(lineData.LineRenderer, position, index)));
        }

        onEnd?.Invoke();
    }

    private IEnumerator DoPathMove(Transform moving, Transform lineTransform, float duration, Vector3[] points,
        Action<Vector3, int> onPointMoveEnd)
    {
        return points.Select((t, i) => StartCoroutine(DoMove(moving, duration, moving.position,
            lineTransform.TransformPoint(t),
            () => onPointMoveEnd?.Invoke(t, i)))).GetEnumerator();
    }

    public static IEnumerator DoMove(Transform moving, float duration, Vector3 from, Vector3 to, Action onEnd = null)
    {
        for (var time = 0f; time <= 1f; time += Time.deltaTime / duration)
        {
            moving.position = Vector3.Lerp(from, to, time);
            yield return null;
        }

        onEnd?.Invoke();
    }
}