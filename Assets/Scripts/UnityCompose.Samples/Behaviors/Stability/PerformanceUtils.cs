using System;
using System.Collections;
using Compose.Net;
using UnityRandom = UnityEngine.Random;

namespace UnityCompose.Samples.Behaviors.Stability;

public static class PerformanceUtils
{
    private static readonly Color[] Colors =
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
    
    public static Color GetColor(int index) => Colors[index % Colors.Length];
    
    public static IEnumerator MoveRandomlyCoroutine(FloatSize parentSize, Action<Offset> onValueChanged)
    {
        while (float.IsNaN(parentSize.Width) || float.IsNaN(parentSize.Height))
            yield return null;

        var current = new Offset(
            UnityRandom.Range(0f, parentSize.Width),
            UnityRandom.Range(0f, parentSize.Height)
        );

        while (true)
        {
            var target = new Offset(
                UnityRandom.Range(0f, parentSize.Width),
                UnityRandom.Range(0f, parentSize.Height)
            );

            var elapsed = 0f;

            var interval = UnityEngine.Random.Range(0.2f, 0.8f);
            while (elapsed < interval)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / interval);
                var value = Offset.Lerp(current, target, t);
                onValueChanged?.Invoke(value);

                yield return null;
            }

            onValueChanged?.Invoke(target);
            current = target;
        }
    }
}