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
    public static IState<float> __AnimateFloatAsState(float targetValue, Optional<AnimationSpec> animationSpec = default, Optional<float> initialValue = default, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(targetValue) ? 0b_00_00_10 : 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(animationSpec) ? 0b_00_10_00 : 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(initialValue) ? 0b_10_00_00 : 0b_01_00_00;
        return __AnimateValueAsState(targetValue: targetValue, initialValue: initialValue, interpolator: FloatInterpolator, animationSpec: animationSpec, __composer: __composer, __changed: (__dirty & 0b_00_00_00_11) | ((__dirty & 0b_00_00_11_00) << 2) | ((__dirty & 0b_00_11_00_00) << 2));
    }

    public static IState<float> __AnimateFloatAsState(object key, Func<float> targetValueFactory, Optional<AnimationSpec> animationSpec = default, Optional<float> initialValue = default, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_11) == 0)
            __dirty |= __composer.Changed(key) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        if ((__changed & 0b_00_00_11_00) == 0)
            __dirty |= __composer.Changed(targetValueFactory) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        if ((__changed & 0b_00_11_00_00) == 0)
            __dirty |= __composer.Changed(animationSpec) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        if ((__changed & 0b_11_00_00_00) == 0)
            __dirty |= __composer.Changed(initialValue) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        return __AnimateValueAsState(key: key, initialValue: initialValue, targetValueFactory: targetValueFactory, interpolator: FloatInterpolator, animationSpec: animationSpec, __composer: __composer, __changed: (__dirty & 0b_00_00_00_00_11) | (__dirty & 0b_00_00_00_11_00) | ((__dirty & 0b_00_00_11_00_00) << 2) | ((__dirty & 0b_00_11_00_00_00) << 2));
    }

    public static IState<Vector2> __AnimateVector2AsState(Vector2 targetValue, Optional<AnimationSpec> animationSpec = default, Optional<Vector2> initialValue = default, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(targetValue) ? 0b_00_00_10 : 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(animationSpec) ? 0b_00_10_00 : 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(initialValue) ? 0b_10_00_00 : 0b_01_00_00;
        return __AnimateValueAsState(targetValue: targetValue, initialValue: initialValue, interpolator: Vector2Interpolator, animationSpec: animationSpec, __composer: __composer, __changed: (__dirty & 0b_00_00_00_11) | ((__dirty & 0b_00_00_11_00) << 2) | ((__dirty & 0b_00_11_00_00) << 2));
    }

    public static IState<Vector2> __AnimateVector2AsState<TKey>(TKey key, Func<Vector2> targetValueFactory, Optional<AnimationSpec> animationSpec = default, Optional<Vector2> initialValue = default, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_11) == 0)
            __dirty |= __composer.Changed(key) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        if ((__changed & 0b_00_00_11_00) == 0)
            __dirty |= __composer.Changed(targetValueFactory) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        if ((__changed & 0b_00_11_00_00) == 0)
            __dirty |= __composer.Changed(animationSpec) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        if ((__changed & 0b_11_00_00_00) == 0)
            __dirty |= __composer.Changed(initialValue) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        return __AnimateValueAsState(key: key, initialValue: initialValue, targetValueFactory: targetValueFactory, interpolator: Vector2Interpolator, animationSpec: animationSpec, __composer: __composer, __changed: (__dirty & 0b_00_00_00_00_11) | (__dirty & 0b_00_00_00_11_00) | ((__dirty & 0b_00_00_11_00_00) << 2) | ((__dirty & 0b_00_11_00_00_00) << 2));
    }

    public static IState<Color> __AnimateColorAsState(Color targetValue, Optional<AnimationSpec> animationSpec = default, Optional<Color> initialValue = default, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(targetValue) ? 0b_00_00_10 : 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(animationSpec) ? 0b_00_10_00 : 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(initialValue) ? 0b_10_00_00 : 0b_01_00_00;
        return __AnimateValueAsState(targetValue: targetValue, initialValue: initialValue, interpolator: static (initial, target, progress) => Color.LerpUnclamped(initial, target, progress), animationSpec: animationSpec, __composer: __composer, __changed: (__dirty & 0b_00_00_00_11) | ((__dirty & 0b_00_00_11_00) << 2) | ((__dirty & 0b_00_11_00_00) << 2));
    }

    public static IState<Color> __AnimateColorAsState<TKey>(TKey key, Func<Color> targetValueFactory, Optional<AnimationSpec> animationSpec = default, Optional<Color> initialValue = default, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_11) == 0)
            __dirty |= __composer.Changed(key) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        if ((__changed & 0b_00_00_11_00) == 0)
            __dirty |= __composer.Changed(targetValueFactory) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        if ((__changed & 0b_00_11_00_00) == 0)
            __dirty |= __composer.Changed(animationSpec) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        if ((__changed & 0b_11_00_00_00) == 0)
            __dirty |= __composer.Changed(initialValue) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        return __AnimateValueAsState(key: key, targetValueFactory: targetValueFactory, initialValue: initialValue, interpolator: static (initial, target, progress) => Color.LerpUnclamped(initial, target, progress), animationSpec: animationSpec, __composer: __composer, __changed: (__dirty & 0b_00_00_00_00_11) | (__dirty & 0b_00_00_00_11_00) | ((__dirty & 0b_00_00_11_00_00) << 2) | ((__dirty & 0b_00_11_00_00_00) << 2));
    }

    public static IState<T> __AnimateValueAsState<T>(T targetValue, Func<T, T, float, T> interpolator, Optional<AnimationSpec> animationSpec = default, Optional<T> initialValue = default, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__targetValue, __interpolator, __animationSpec, __initialValue) = (targetValue, interpolator, animationSpec, initialValue);
        __composer.StartReplaceGroup(237772736);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_11) == 0)
            __dirty |= __composer.Changed(targetValue) ? 0b_00_00_00_10 : 0b_00_00_00_01;
        if ((__changed & 0b_00_00_11_00) == 0)
            __dirty |= __composer.Changed(interpolator) ? 0b_00_00_10_00 : 0b_00_00_01_00;
        if ((__changed & 0b_00_11_00_00) == 0)
            __dirty |= __composer.Changed(animationSpec) ? 0b_00_10_00_00 : 0b_00_01_00_00;
        if ((__changed & 0b_11_00_00_00) == 0)
            __dirty |= __composer.Changed(initialValue) ? 0b_10_00_00_00 : 0b_01_00_00_00;
        var property = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<T>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<T>>(MutableStateOf(initialValue.GetOrDefault(targetValue))));
        if (!ApplicationUtils.IsPlaying)
        {
            property.Value = targetValue;
            __composer.EndReplaceGroup(237772736);
            return property;
        }

        if (EqualityUtils.FastEquals(property.Value, targetValue))
        {
            __composer.EndReplaceGroup(237772736);
            return property;
        }

        __LaunchedEffect(key: targetValue, // block: () => property.Value = targetValue
        coroutine: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_11) == 0b_00_00_00_10).Changed<global::UnityCompose.IMutableState<T>>(property!).Get() ? __composer.RememberedValue<global::System.Func<global::System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<global::System.Func<global::System.Collections.IEnumerator>>(() => UpdatePropertyCoroutine(targetValue))), __composer: __composer, __changed: (__dirty & 0b_00_11));
        __composer.EndReplaceGroup(237772736);
        return property;
        IEnumerator UpdatePropertyCoroutine(T newValue)
        {
            yield return null;
            var resolvedAnimationSpec = animationSpec.GetOrDefault();
            var startValue = property.GetValue();
            if (EqualityUtils.FastEquals(startValue, targetValue))
                yield break;
            if (resolvedAnimationSpec.Delay > 0)
                yield return new WaitForSeconds(resolvedAnimationSpec.Delay);
            var elapsed = resolvedAnimationSpec.Delay;
            while (elapsed < resolvedAnimationSpec.TotalDuration)
            {
                elapsed += Time.deltaTime;
                property.Value = interpolator(startValue, newValue, resolvedAnimationSpec.GetProgress(elapsed));
                yield return null;
            }

            property.Value = newValue;
        }
    }

    public static IState<T> __AnimateValueAsState<TKey, T>(TKey key, Func<T> targetValueFactory, Func<T, T, float, T> interpolator, Optional<AnimationSpec> animationSpec = default, Optional<T> initialValue = default, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__key, __targetValueFactory, __interpolator, __animationSpec, __initialValue) = (key, targetValueFactory, interpolator, animationSpec, initialValue);
        __composer.StartReplaceGroup(1531940465);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(key) ? 0b_00_00_00_00_10 : 0b_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(targetValueFactory) ? 0b_00_00_00_10_00 : 0b_00_00_00_01_00;
        if ((__changed & 0b_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(interpolator) ? 0b_00_00_10_00_00 : 0b_00_00_01_00_00;
        if ((__changed & 0b_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(animationSpec) ? 0b_00_10_00_00_00 : 0b_00_01_00_00_00;
        if ((__changed & 0b_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(initialValue) ? 0b_10_00_00_00_00 : 0b_01_00_00_00_00;
        var targetValue = targetValueFactory();
        var property = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<T>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<T>>(MutableStateOf(initialValue.GetOrDefault(targetValue))));
        if (!ApplicationUtils.IsPlaying)
        {
            property.Value = targetValue;
            __composer.EndReplaceGroup(1531940465);
            return property;
        }

        if (EqualityUtils.FastEquals(property.Value, targetValue))
        {
            __composer.EndReplaceGroup(1531940465);
            return property;
        }

        var resolvedAnimationSpec = animationSpec.GetOrDefault();
        __LaunchedEffect(key: key, coroutine: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_11_00) == 0b_00_00_00_10_00).Changed<global::UnityCompose.IMutableState<T>>(property!).Get() ? __composer.RememberedValue<global::System.Func<global::System.Collections.IEnumerator>>() : __composer.UpdateRememberedValue<global::System.Func<global::System.Collections.IEnumerator>>(() => UpdatePropertyCoroutine(targetValueFactory))), __composer: __composer, __changed: (__dirty & 0b_00_11));
        __composer.EndReplaceGroup(1531940465);
        return property;
        IEnumerator UpdatePropertyCoroutine(Func<T> newValueFactory)
        {
            yield return null;
            var startValue = property.GetValue();
            if (Equals(startValue, newValueFactory()))
                yield break;
            if (resolvedAnimationSpec.Delay > 0)
                yield return new WaitForSeconds(resolvedAnimationSpec.Delay);
            var elapsed = resolvedAnimationSpec.Delay;
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