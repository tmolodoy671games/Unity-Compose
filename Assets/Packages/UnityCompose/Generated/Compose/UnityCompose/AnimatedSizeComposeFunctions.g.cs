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
    [Composable]
    private static void __AnimatedSize(ComposableContent<IModifier> content, IModifier? modifier = null, Optional<AnimationSpec> animationSpec = default)
    {
        var(__content, __modifier, __animationSpec) = (content, modifier, animationSpec);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1035912844);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__content, __modifier, __animationSpec)))
        {
            var resolvedAnimationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
            var(containerModifier, contentModifier) = AnimateSizeModifiers(resolvedAnimationSpec.GetOrDefault());
            ReusableComposeView<AnimatedSize>(modifier: modifier.OrEmpty().Then(containerModifier), initializer: !__composer.Changed() ? __composer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.AnimatedSize>?>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.AnimatedSize>?>(it =>
            {
                it.style.alignItems = Align.Center;
                it.style.justifyContent = Justify.Center;
            }), content: !__composer.ChangedAsStruct((content, contentModifier)) ? __composer.RememberedValue<UnityCompose.ComposableContent?>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent?>(() =>
            {
                content(contentModifier);
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1035912844, __isRestarted)?.UpdateScope(() => __AnimatedSize(__content, __modifier, __animationSpec));
    }

    [Composable]
    private static (IModifier ContainerModifier, IModifier ContentModifier) __AnimateSizeModifiers(AnimationSpec animationSpec, object? key = null)
    {
        var __composer = CurrentComposer;
        var containerPaddings = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>(MutableStateOf(new Vector2(-1, -1)));
        var contentSize = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>(MutableStateOf(new Vector2(-1, -1)));
        var contentModifier = Modifier;
        var containerModifier = Modifier.Clip().OnLocallyPositioned(!__composer.Changed(containerPaddings) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it =>
        {
            containerPaddings.Value = new Vector2(it.PaddingLeft + it.PaddingRight, it.PaddingTop + it.PaddingBottom).Approximate();
        }));
        __composer.StartReplaceGroup(-1909905094);
        if (!IsInPreview)
        {
            contentModifier = contentModifier.OnLocallyPositioned(!__composer.Changed(contentSize) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it =>
            {
                var resolvedSize = it.SizeWithPaddings;
                resolvedSize += Vector2.right * (it.MarginLeft + it.MarginRight);
                resolvedSize += Vector2.up * (it.MarginTop + it.MarginBottom);
                contentSize.Value = resolvedSize.Approximate();
            }));
            var isSizeValid = contentSize.Value is { x: > 0, y: > 0 } && containerPaddings.Value is { x: >= 0, y: >= 0 };
            __composer.StartReplaceGroup(-2106743383);
            if (isSizeValid)
            {
                var animatedSize = key != null ? __composer.WithReplaceGroup(-1104282356, () => AnimateVector2AsState(key: key, targetValueFactory: !__composer.ChangedAsStruct((containerPaddings, contentSize)) ? __composer.RememberedValue<System.Func<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<System.Func<UnityEngine.Vector2>>(() => contentSize.Value + containerPaddings.Value), animationSpec: animationSpec).Value) : __composer.WithReplaceGroup(1065411164, () => AnimateVector2AsState(targetValue: contentSize.Value + containerPaddings.Value, animationSpec: animationSpec).Value);
                containerModifier = containerModifier.Size(width: animatedSize.x.Px(), height: animatedSize.y.Px());
                contentModifier = contentModifier.Float();
            }

            __composer.EndReplaceGroup(-2106743383);
        }

        __composer.EndReplaceGroup(-1909905094);
        return (containerModifier, contentModifier);
    }
}