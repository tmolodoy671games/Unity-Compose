#nullable enable
using System;
using System.Collections;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
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
        var(__targetValue, __interpolator, __animationSpec) = (targetValue, interpolator, animationSpec);
        var __composer = CurrentComposer;
        __composer.StartReplaceGroup(-130532738);
        var property = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<T>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<T>>(MutableStateOf(targetValue));
        if (!ApplicationUtils.IsPlaying)
        {
            property.Value = targetValue;
            __composer.EndReplaceGroup(-130532738);
            return property;
        }

        if (EqualityUtils.FastEquals(property.Value, targetValue))
        {
            __composer.EndReplaceGroup(-130532738);
            return property;
        }

        LaunchedEffect(key: targetValue, coroutine: !__composer.ChangedAsStruct((targetValue, property)) ? __composer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<System.Func<System.Collections.IEnumerator>>(() => UpdatePropertyCoroutine(targetValue)));
        __composer.EndReplaceGroup(-130532738);
        return property;
        IEnumerator UpdatePropertyCoroutine(T newValue)
        {
            yield return null;
            var resolvedAnimationSpec = animationSpec.GetOrDefault();
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
        var(__key, __targetValueFactory, __interpolator, __animationSpec) = (key, targetValueFactory, interpolator, animationSpec);
        var __composer = CurrentComposer;
        __composer.StartReplaceGroup(-1673470175);
        var targetValue = targetValueFactory();
        var property = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<T>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<T>>(MutableStateOf(targetValue));
        if (!ApplicationUtils.IsPlaying)
        {
            property.Value = targetValue;
            __composer.EndReplaceGroup(-1673470175);
            return property;
        }

        if (EqualityUtils.FastEquals(property.Value, targetValue))
        {
            __composer.EndReplaceGroup(-1673470175);
            return property;
        }

        var resolvedAnimationSpec = animationSpec.GetOrDefault();
        LaunchedEffect(key: key, coroutine: !__composer.ChangedAsStruct((targetValueFactory, property)) ? __composer.RememberedValue<System.Func<System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<System.Func<System.Collections.IEnumerator>>(() => UpdatePropertyCoroutine(targetValueFactory)));
        __composer.EndReplaceGroup(-1673470175);
        return property;
        IEnumerator UpdatePropertyCoroutine(Func<T> newValueFactory)
        {
            yield return null;
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