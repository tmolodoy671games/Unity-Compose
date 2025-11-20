using System;
using System.Collections;
using Random = System.Random;

namespace UnityCompose.Samples.Behaviors;

public static class PerformanceUtils
{
    public static readonly Color[] Colors =
    {
        Color.red,
        Color.green,
        Color.blue,
        Color.yellow,
        Color.cyan,
        Color.magenta,
        Color.white,
        Color.gray,
    };
    
    public static IEnumerator MoveRandomlyCoroutine(Func<Vector2> parentSize, Action<Vector2> onValueChanged)
    {
        while (float.IsNaN(parentSize().x) || float.IsNaN(parentSize().y))
            yield return null;

        var current = new Vector2(
            UnityEngine.Random.Range(0f, parentSize().x),
            UnityEngine.Random.Range(0f, parentSize().y)
        );

        while (true)
        {
            var target = new Vector2(
                UnityEngine.Random.Range(0f, parentSize().x),
                UnityEngine.Random.Range(0f, parentSize().y)
            );

            var elapsed = 0f;

            var interval = UnityEngine.Random.Range(0.2f, 0.8f);
            while (elapsed < interval)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / interval);
                var value = Vector2.Lerp(current, target, t);
                onValueChanged?.Invoke(value);

                yield return null;
            }

            onValueChanged?.Invoke(target);
            current = target;
        }
    }
    
    public static IEnumerator MeasureFpsCoroutine(Action<int> onValueChanged)
    {
        var interval = 1f;

        var frames = 0;
        var elapsed = 0f;

        while (true)
        {
            frames++;
            elapsed += Time.deltaTime;

            if (elapsed >= interval)
            {
                var fps = Mathf.RoundToInt(frames / elapsed);
                onValueChanged?.Invoke(fps);

                frames = 0;
                elapsed = 0f;
            }

            yield return null;
        }
    }
}