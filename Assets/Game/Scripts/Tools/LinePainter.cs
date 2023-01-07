using System;
using System.Collections;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class LinePainter : ManagerBase
{
    public void Draw(Pen pen, LineData[] linesData, float duration, Action onEnd = null)
    {
        pen.ActivePen(linesData[0].Transform.TransformPoint(linesData[0].Points[0]), () =>
        {
            foreach (var lineData in linesData) lineData.SetGradient(pen.PenGradient);
            StartCoroutine(PenDrawCoroutine(pen.Transform, linesData, duration, () => { pen.DeactivatePen(onEnd); }));
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
            yield return StartCoroutine(DoPath(pen, lineData.Transform, duration, lineData.Points,
                (position, index) => SetLinePosition(lineData.LineRenderer, position, index)));
        }

        onEnd?.Invoke();
    }

    private IEnumerator DoPathMove(Transform moving, Transform lineTransform, float duration, Vector3[] points,
        Action<Vector3, int> onPointMoveEnd)
    {
        return points.Select((t, i) => StartCoroutine(DoMove(moving, duration, lineTransform.TransformPoint(t),
            () => onPointMoveEnd?.Invoke(t, i)))).GetEnumerator();
    }

    public static IEnumerator DoMove(Transform moving, float duration, Vector3 to, Action onEnd = null)
    {
        var tween = moving.DOMove(to, duration);
        yield return tween.WaitForCompletion();
        onEnd?.Invoke();
    }

    private IEnumerator DoPath(Transform moving, Transform lineTransform, float duration, Vector3[] points,
        Action<Vector3, int> onPointMoveEnd)
    {
        var transformPoints = points.Select(lineTransform.TransformPoint).ToArray();
        var tween = moving.DOPath(transformPoints, duration, PathType.Linear, PathMode.Ignore);
        tween.onWaypointChange += (index) =>
        {
            if (index.Equals(points.Length)) return;
            onPointMoveEnd?.Invoke(points[index], index);
        };
        yield return tween.WaitForCompletion();
    }
}