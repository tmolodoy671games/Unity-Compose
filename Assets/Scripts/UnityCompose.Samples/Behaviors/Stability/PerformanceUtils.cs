using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using Compose.Net;
using SharpExtensions;
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

            var interval = UnityEngine.Random.Range(1f, 2f);
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

    public static async Task MoveRandomlyCoroutine(
        FloatSize parentSize,
        Action<Offset> onValueChanged,
        CancellationToken token
    )
    {
        if (float.IsNaN(parentSize.Width) || float.IsNaN(parentSize.Height))
            return;

        var current = new Offset(
            UnityRandom.Range(0f, parentSize.Width),
            UnityRandom.Range(0f, parentSize.Height)
        );

        while (true)
        {
            await NextFrame(token);
            var target = new Offset(
                UnityRandom.Range(0f, parentSize.Width),
                UnityRandom.Range(0f, parentSize.Height)
            );
            var interval = UnityEngine.Random.Range(1, 2f);
            await Animate(
                typeConverter: Offset.LerpUnclamped,
                initialValue: current,
                targetValue: target,
                block: it => onValueChanged?.Invoke(it),
                animationSpec: Tween(TimeSpan.FromSeconds(interval)),
                cancellationToken: token
            );

            onValueChanged?.Invoke(target);
            current = target;
        }
    }
}