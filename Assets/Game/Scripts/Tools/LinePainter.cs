using System;
using System.Collections;
using UnityEngine;

public class LinePainter : ManagerBase
{
    [SerializeField] private Transform _pen;

    public void Draw(LineData[] linesData, float duration) => StartCoroutine(PenDrawCoroutine(linesData, duration));


    private void SetLinePosition(LineRenderer line, Vector3 position, int index)
    {
        line.positionCount = index + 1;
        line.SetPosition(index, position);
    }

    private IEnumerator PenDrawCoroutine(LineData[] linesData, float duration)
    {
        for (int i = 0; i < linesData.Length; i++)
        {
            yield return StartCoroutine(DoPathMove(_pen, linesData[i].Transform, duration, linesData[i].Points,
                (position, index) => SetLinePosition(linesData[i].LineRenderer, position, index)));
        }
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

    private IEnumerator DoMove(Transform moving, float duration, Vector3 from, Vector3 to, Action onEnd)
    {
        for (float time = 0f; time <= 1f; time += Time.deltaTime / duration)
        {
            moving.position = Vector3.Lerp(from, to, time);
            yield return null;
        }

        onEnd?.Invoke();
    }
}