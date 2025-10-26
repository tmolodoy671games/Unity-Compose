using System;
using System.Collections;
using SharpExtensions;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    private static readonly Func<float, float, float, float> FloatInterpolator =
        (startValue, targetValue, progress) => startValue + (targetValue - startValue) * progress;

    private static readonly Func<Vector2, Vector2, float, Vector2> Vector2Interpolator =
        (startValue, targetValue, progress) => startValue + (targetValue - startValue) * progress;

    [Composable, Compiled]
    public static IState<float> AnimateFloatAsState(
        float targetValue,
        float duration = ComposeDefaults.TransitionDuration,
        IEasing? easing = null
    )
    {
        return AnimateValueAsState(
            targetValue: targetValue,
            interpolator: FloatInterpolator,
            duration: duration,
            easing: easing
        );
    }
    
    [Composable, Compiled]
    public static IState<float> AnimateFloatAsState(
        object key,
        Func<float> targetValueFactory,
        float duration = ComposeDefaults.TransitionDuration,
        IEasing? easing = null
    )
    {
        return AnimateValueAsState(
            key: key,
            targetValueFactory: targetValueFactory,
            interpolator: FloatInterpolator,
            duration: duration,
            easing: easing
        );
    }

    [Composable, Compiled]
    public static IState<Vector2> AnimateVector2AsState(
        Vector2 targetValue,
        float duration = ComposeDefaults.TransitionDuration,
        IEasing? easing = null
    )
    {
        return AnimateValueAsState(
            targetValue: targetValue,
            interpolator: Vector2Interpolator,
            duration: duration,
            easing: easing
        );
    }
    
    [Composable, Compiled]
    public static IState<Vector2> AnimateVector2AsState(
        object key,
        Func<Vector2> targetValueFactory,
        float duration = ComposeDefaults.TransitionDuration,
        IEasing? easing = null
    )
    {
        return AnimateValueAsState(
            key: key,
            targetValueFactory: targetValueFactory,
            interpolator: Vector2Interpolator,
            duration: duration,
            easing: easing
        );
    }

    [Composable, Compiled]
    public static IState<T> AnimateValueAsState<T>(
        T targetValue,
        Func<T, T, float, T> interpolator,
        IEasing? easing = null,
        float duration = ComposeDefaults.TransitionDuration
    )
    {
        var property = Remember(() => MutableStateOf(targetValue));
        if (Equals(property.Value, targetValue)) return property;

        LaunchedEffect(
            key: targetValue!,
            coroutine: UpdatePropertyCoroutine(targetValue)
        );
        return property;

        IEnumerator UpdatePropertyCoroutine(T newValue)
        {
            var startValue = property.Value;
            var curve = easing ?? EaseInOut;
            if (Equals(startValue, targetValue)) yield break;
            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                property.Value = interpolator(startValue, newValue, curve.Transform(t));
                yield return null;
            }

            property.Value = newValue;
        }
    }
    
    [Composable, Compiled]
    public static IState<T> AnimateValueAsState<T>(
        object key,
        Func<T> targetValueFactory,
        Func<T, T, float, T> interpolator,
        IEasing? easing = null,
        float duration = ComposeDefaults.TransitionDuration
    )
    {
        var targetValue = targetValueFactory();
        var property = Remember(() => MutableStateOf(targetValue));
        if (EqualityUtils.FastEquals(property.Value, targetValue)) return property;

        LaunchedEffect(
            key: key,
            coroutine: UpdatePropertyCoroutine(targetValueFactory)
        );
        return property;

        IEnumerator UpdatePropertyCoroutine(Func<T> newValueFactory)
        {
            var startValue = property.Value;
            var curve = easing ?? EaseInOut;
            if (Equals(startValue, newValueFactory())) yield break;
            var elapsed = 0f;
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                property.Value = interpolator(startValue, newValueFactory(), curve.Transform(t));
                yield return null;
            }

            property.Value = newValueFactory();
        }
    }
}