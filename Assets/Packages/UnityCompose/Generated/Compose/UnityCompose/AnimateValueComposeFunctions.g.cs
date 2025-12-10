using System;
using System.Collections;
using SharpExtensions;
using UnityEngine;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static IState<float> __AnimateFloatAsState(float targetValue, Optional<AnimationSpec> animationSpec = default)
    {
        var __composer = CurrentComposer;
        return AnimateValueAsState(targetValue: targetValue, interpolator: FloatInterpolator, animationSpec: animationSpec);
    }

    [Composable]
    private static IState<float> __AnimateFloatAsState(object key, Func<float> targetValueFactory, Optional<AnimationSpec> animationSpec = default)
    {
        var __composer = CurrentComposer;
        return AnimateValueAsState(key: key, targetValueFactory: targetValueFactory, interpolator: FloatInterpolator, animationSpec: animationSpec);
    }

    [Composable]
    private static IState<Vector2> __AnimateVector2AsState(Vector2 targetValue, Optional<AnimationSpec> animationSpec = default)
    {
        var __composer = CurrentComposer;
        return AnimateValueAsState(targetValue: targetValue, interpolator: Vector2Interpolator, animationSpec: animationSpec);
    }

    [Composable]
    private static IState<Color> __AnimateColorAsState(Color targetValue, Optional<AnimationSpec> animationSpec = default)
    {
        var __composer = CurrentComposer;
        return AnimateValueAsState(targetValue: targetValue, interpolator: static (initial, target, progress) => Color.LerpUnclamped(initial, target, progress), animationSpec: animationSpec);
    }

    [Composable]
    private static IState<Vector2> __AnimateVector2AsState(object key, Func<Vector2> targetValueFactory, Optional<AnimationSpec> animationSpec = default)
    {
        var __composer = CurrentComposer;
        return AnimateValueAsState(key: key, targetValueFactory: targetValueFactory, interpolator: Vector2Interpolator, animationSpec: animationSpec);
    }

    [Composable]
    private static IState<T> __AnimateValueAsState<T>(T targetValue, Func<T, T, float, T> interpolator, Optional<AnimationSpec> animationSpec = default)
    {
        var __composer = CurrentComposer;
        var property = !__composer.RememberedKeyChanged<bool>(-1967881308, true) ? __composer.RememberedValue<UnityCompose.IMutableState<T>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<T>>(MutableStateOf(targetValue));
        if (EqualityUtils.FastEquals(property.Value, targetValue))
            return property;
        var resolvedAnimationSpec = animationSpec.GetOrDefault();
        LaunchedEffect(key: targetValue!, coroutine: !__composer.RememberedKeyChanged<ValueTuple<T, UnityCompose.IMutableState<T>?>>(680197389, (targetValue, property)) ? CurrentComposer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : CurrentComposer.UpdateLambda<System.Func<System.Collections.IEnumerator>>(() => UpdatePropertyCoroutine(targetValue)));
        return property;
        IEnumerator UpdatePropertyCoroutine(T newValue)
        {
            var startValue = property.Value;
            if (EqualityUtils.FastEquals(startValue, targetValue))
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
    private static IState<T> __AnimateValueAsState<T>(object key, Func<T> targetValueFactory, Func<T, T, float, T> interpolator, Optional<AnimationSpec> animationSpec = default)
    {
        var __composer = CurrentComposer;
        var targetValue = targetValueFactory();
        var property = !__composer.RememberedKeyChanged<bool>(-1382118261, true) ? __composer.RememberedValue<UnityCompose.IMutableState<T>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<T>>(MutableStateOf(targetValue));
        if (EqualityUtils.FastEquals(property.Value, targetValue))
            return property;
        var resolvedAnimationSpec = animationSpec.GetOrDefault();
        LaunchedEffect(key: key, coroutine: !__composer.RememberedKeyChanged<ValueTuple<System.Func<T>, UnityCompose.IMutableState<T>?>>(1303380056, (targetValueFactory, property)) ? CurrentComposer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : CurrentComposer.UpdateLambda<System.Func<System.Collections.IEnumerator>>(() => UpdatePropertyCoroutine(targetValueFactory)));
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