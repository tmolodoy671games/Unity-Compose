using System;
using System.Collections;
using SharpExtensions;
using UnityEngine;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    [Compiled]
    private static IState<float> __AnimateFloatAsState(float targetValue, Optional<AnimationSpec> animationSpec = default)
    {
        return AnimateValueAsState(targetValue: targetValue, interpolator: FloatInterpolator, animationSpec: animationSpec);
    }

    [Composable]
    [Compiled]
    private static IState<float> __AnimateFloatAsState(object key, Func<float> targetValueFactory, Optional<AnimationSpec> animationSpec = default)
    {
        return AnimateValueAsState(key: key, targetValueFactory: targetValueFactory, interpolator: FloatInterpolator, animationSpec: animationSpec);
    }

    [Composable]
    [Compiled]
    private static IState<Vector2> __AnimateVector2AsState(Vector2 targetValue, Optional<AnimationSpec> animationSpec = default)
    {
        return AnimateValueAsState(targetValue: targetValue, interpolator: Vector2Interpolator, animationSpec: animationSpec);
    }

    [Composable]
    [Compiled]
    private static IState<Color> __AnimateColorAsState(Color targetValue, Optional<AnimationSpec> animationSpec = default)
    {
        return AnimateValueAsState(targetValue: targetValue, interpolator: Remember<global::System.Func<global::UnityEngine.Color, global::UnityEngine.Color, float, global::UnityEngine.Color>>(null, static (initial, target, progress) => Color.LerpUnclamped(initial, target, progress)), animationSpec: animationSpec);
    }

    [Composable]
    [Compiled]
    private static IState<Vector2> __AnimateVector2AsState(object key, Func<Vector2> targetValueFactory, Optional<AnimationSpec> animationSpec = default)
    {
        return AnimateValueAsState(key: key, targetValueFactory: targetValueFactory, interpolator: Vector2Interpolator, animationSpec: animationSpec);
    }

    [Composable]
    [Compiled]
    private static IState<T> __AnimateValueAsState<T>(T targetValue, Func<T, T, float, T> interpolator, Optional<AnimationSpec> animationSpec = default)
    {
        var property = Remember(() => MutableStateOf(targetValue));
        if (Equals(property.Value, targetValue))
            return property;
        var resolvedAnimationSpec = animationSpec.GetOrDefault();
        LaunchedEffect(key: targetValue!, coroutine: UpdatePropertyCoroutine(targetValue));
        return property;
        IEnumerator UpdatePropertyCoroutine(T newValue)
        {
            var startValue = property.Value;
            if (Equals(startValue, targetValue))
                yield break;
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
    [Compiled]
    private static IState<T> __AnimateValueAsState<T>(object key, Func<T> targetValueFactory, Func<T, T, float, T> interpolator, Optional<AnimationSpec> animationSpec = default)
    {
        var targetValue = targetValueFactory();
        var property = Remember(() => MutableStateOf(targetValue));
        if (EqualityUtils.FastEquals(property.Value, targetValue))
            return property;
        var resolvedAnimationSpec = animationSpec.GetOrDefault();
        LaunchedEffect(key: key, coroutine: UpdatePropertyCoroutine(targetValueFactory));
        return property;
        IEnumerator UpdatePropertyCoroutine(Func<T> newValueFactory)
        {
            var startValue = property.Value;
            if (Equals(startValue, newValueFactory()))
                yield break;
            if (resolvedAnimationSpec.Delay > 0)
                yield return new WaitForSeconds(resolvedAnimationSpec.Delay);
            var elapsed = 0f;
            while (elapsed < resolvedAnimationSpec.TotalDuration)
            {
                elapsed += Time.deltaTime;
                property.Value = interpolator(startValue, newValueFactory(), resolvedAnimationSpec.GetProgress(elapsed));
                yield return null;
            }

            property.Value = newValueFactory();
        }
    }
}