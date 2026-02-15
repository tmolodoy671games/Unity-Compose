#nullable enable
using System.Diagnostics.CodeAnalysis;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    public static void __AnimatedSize(ComposableContent<IModifier> content, IModifier? modifier = null, Optional<AnimationSpec> animationSpec = default, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__content, __modifier, __animationSpec) = (content, modifier, animationSpec);
        var __isCreated = __composer.StartRestartGroup(1035912844);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(content) ? 0b_00_00_10 : 0b_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01;
        }

        if ((__changed & 0b_00_11_00) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_00_10_00 : 0b_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00;
        }

        if ((__changed & 0b_11_00_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(animationSpec) ? 0b_10_00_00 : 0b_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            var resolvedAnimationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
            var(containerModifier, contentModifier) = __AnimateSizeModifiers(resolvedAnimationSpec.GetOrDefault(), __composer: __composer, __changed: 0b_01_00);
            __ReusableComposeView<AnimatedSize>(modifier: modifier.OrEmpty().Then(containerModifier), initializer: (!__composer.Changed() ? __composer.RememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.AnimatedSize>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.AnimatedSize>>(it =>
            {
                it.style.alignItems = Align.Center;
                it.style.justifyContent = Justify.Center;
            })), content: (!__composer.ChangedAsStruct((content, contentModifier)) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                content(contentModifier);
            })), __composer: __composer, __changed: 0);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1035912844, __isRestarted)?.UpdateScope(() => __AnimatedSize(__content, __modifier, __animationSpec, __composer, __dirtyRestart));
    }

    private static (IModifier ContainerModifier, IModifier ContentModifier) __AnimateSizeModifiers(AnimationSpec animationSpec, object? key = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_11) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(animationSpec) ? 0b_00_10 : 0b_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_01;
        }

        if ((__changed & 0b_11_00) == 0)
        {
            __dirty |= __composer.Changed(key) ? 0b_10_00 : 0b_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00;
        }

        var containerPaddings = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>(MutableStateOf(new Vector2(-1, -1))));
        var contentSize = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<global::UnityEngine.Vector2>>(MutableStateOf(new Vector2(-1, -1))));
        var contentModifier = Modifier;
        var containerModifier = Modifier.Clip().OnLocallyPositioned((!__composer.Changed(containerPaddings) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it =>
        {
            containerPaddings.Value = new Vector2(it.PaddingLeft + it.PaddingRight, it.PaddingTop + it.PaddingBottom).Approximate();
        })));
        __composer.StartReplaceGroup(1909905094);
        if (!IsInPreview)
        {
            contentModifier = contentModifier.OnLocallyPositioned((!__composer.Changed(contentSize) ? __composer.RememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<global::System.Action<global::UnityCompose.LayoutCoordinates>>(it =>
            {
                var resolvedSize = it.Size;
                resolvedSize += Vector2.right * (it.MarginLeft + it.MarginRight);
                resolvedSize += Vector2.up * (it.MarginTop + it.MarginBottom);
                contentSize.Value = resolvedSize.Approximate();
            })));
            var isSizeValid = contentSize.Value is { x: > 0, y: > 0 } && containerPaddings.Value is { x: >= 0, y: >= 0 };
            __composer.StartReplaceGroup(2106743383);
            if (isSizeValid)
            {
                var animatedSize = key != null ? __composer.WithReplaceGroup(1104282356, () => __AnimateVector2AsState(key: key, targetValueFactory: (!__composer.ChangedAsStruct((containerPaddings, contentSize)) ? __composer.RememberedValue<global::System.Func<global::UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<global::System.Func<global::UnityEngine.Vector2>>(() => contentSize.Value + containerPaddings.Value)), animationSpec: animationSpec, __composer: __composer, __changed: ((__dirty & 0b_00_11_00) >> 2) | ((__dirty & 0b_00_00_11) << 4)).Value) : __composer.WithReplaceGroup(1065411164, () => __AnimateVector2AsState(targetValue: contentSize.Value + containerPaddings.Value, animationSpec: animationSpec, __composer: __composer, __changed: ((__dirty & 0b_00_11) << 2)).Value);
                containerModifier = containerModifier.Size(width: animatedSize.x.Px(), height: animatedSize.y.Px());
                contentModifier = contentModifier.Float();
            }

            __composer.EndReplaceGroup(2106743383);
        }

        __composer.EndReplaceGroup(1909905094);
        return (containerModifier, contentModifier);
    }
}