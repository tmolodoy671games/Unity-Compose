using System;
using System.Collections;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    private static readonly Func<float, float, float, float> FloatInterpolator =
        (startValue, targetValue, progress) => startValue + (targetValue - startValue) * progress;

    private static readonly Func<Vector2, Vector2, float, Vector2> Vector2Interpolator =
        (startValue, targetValue, progress) => startValue + (targetValue - startValue) * progress;

    [Composable]
    public static IState<float> AnimateFloatAsState(
        float targetValue,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return AnimateValueAsState(
            targetValue: targetValue,
            interpolator: FloatInterpolator,
            animationSpec: animationSpec
        );
    }

    [Composable]
    public static IState<float> AnimateFloatAsState(
        object key,
        Func<float> targetValueFactory,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return AnimateValueAsState(
            key: key,
            targetValueFactory: targetValueFactory,
            interpolator: FloatInterpolator,
            animationSpec: animationSpec
        );
    }

    [Composable]
    public static IState<Vector2> AnimateVector2AsState(
        Vector2 targetValue,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return AnimateValueAsState(
            targetValue: targetValue,
            interpolator: Vector2Interpolator,
            animationSpec: animationSpec
        );
    }

    [Composable]
    public static IState<Color> AnimateColorAsState(
        Color targetValue,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return AnimateValueAsState(
            targetValue: targetValue,
            interpolator: static (initial, target, progress) => Color.LerpUnclamped(initial, target, progress),
            animationSpec: animationSpec
        );
    }

    [Composable]
    public static IState<Vector2> AnimateVector2AsState(
        object key,
        Func<Vector2> targetValueFactory,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        return AnimateValueAsState(
            key: key,
            targetValueFactory: targetValueFactory,
            interpolator: Vector2Interpolator,
            animationSpec: animationSpec
        );
    }

    [Composable]
    public static IState<T> AnimateValueAsState<T>(
        T targetValue,
        Func<T, T, float, T> interpolator,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        var property = Remember(() => MutableStateOf(targetValue));
        if (!ApplicationUtils.IsPlaying)
        {
            property.Value = targetValue;
            return property;
        }

        if (EqualityUtils.FastEquals(property.Value, targetValue)) return property;

        LaunchedEffect(
            key: targetValue,
            // block: () => property.Value = targetValue
            coroutine: () => UpdatePropertyCoroutine(targetValue)
        );
        return property;

        IEnumerator UpdatePropertyCoroutine(T newValue)
        {
            yield return null;
            var resolvedAnimationSpec = animationSpec.GetOrDefault();
            var startValue = property.GetValue();
            if (EqualityUtils.FastEquals(startValue, targetValue)) yield break;
            if (resolvedAnimationSpec.Delay > 0)
                yield return new WaitForSeconds(resolvedAnimationSpec.Delay);
            var elapsed = 0f;
            while (elapsed < resolvedAnimationSpec.TotalDuration)
            {
                elapsed += Time.deltaTime;
                property.Value = interpolator(startValue, newValue, resolvedAnimationSpec.GetProgress(elapsed));
                yield return null;
            }

            property.Value = newValue;
        }
    }

    [Composable]
    public static IState<T> AnimateValueAsState<T>(
        object key,
        Func<T> targetValueFactory,
        Func<T, T, float, T> interpolator,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        var targetValue = targetValueFactory();
        var property = Remember(() => MutableStateOf(targetValue));
        if (!ApplicationUtils.IsPlaying)
        {
            property.Value = targetValue;
            return property;
        }

        if (EqualityUtils.FastEquals(property.Value, targetValue)) return property;
        var resolvedAnimationSpec = animationSpec.GetOrDefault();

        LaunchedEffect(
            key: key,
            coroutine: () => UpdatePropertyCoroutine(targetValueFactory)
        );
        return property;

        IEnumerator UpdatePropertyCoroutine(Func<T> newValueFactory)
        {
            yield return null;
            var startValue = property.GetValue();
            if (Equals(startValue, newValueFactory())) yield break;
            if (resolvedAnimationSpec.Delay > 0)
                yield return new WaitForSeconds(resolvedAnimationSpec.Delay);
            var elapsed = 0f;
            while (elapsed < resolvedAnimationSpec.TotalDuration)
            {
                elapsed += Time.deltaTime;
                property.Value = interpolator(startValue, newValueFactory(),
                    resolvedAnimationSpec.GetProgress(elapsed));
                yield return null;
            }

            property.Value = newValueFactory();
        }
    }
}