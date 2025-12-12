using System;
using System.Diagnostics.CodeAnalysis;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __AnimatedSize(ComposableContent content, IModifier? modifier = null, Optional<AnimationSpec> animationSpec = default)
    {
        var(__content, __modifier, __animationSpec) = (content, modifier, animationSpec);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1826694553);
        if (__composer.ShouldExecuteAsStruct((__content, __modifier, __animationSpec)))
        {
            var resolvedAnimationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
            var(containerStyle, contentStyle) = AnimateSizeModifiers(resolvedAnimationSpec.GetOrDefault());
            ReusableComposeView<AnimatedSize>(modifier: modifier.OrEmpty().Then(containerStyle), initializer: !__composer.Changed() ? __composer.RememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.AnimatedSize>?>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.AnimatedSize>?>(it =>
            {
                it.style.alignItems = Align.Center;
                it.style.justifyContent = Justify.Center;
            }), content: !__composer.ChangedAsStruct((content, contentStyle)) ? __composer.RememberedValue<UnityCompose.ComposableContent?>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent?>(() =>
            {
                CompositionLocalProvider(LocalModifier.Provides(after: contentStyle), content: content);
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1826694553)?.UpdateScope(() => __AnimatedSize(__content, __modifier, __animationSpec));
    }

    [Composable]
    private static (IModifier ContainerModifier, IModifier ContentModifier) __AnimateSizeModifiers(AnimationSpec animationSpec, object? key = null)
    {
        var __composer = CurrentComposer;
        var resolvedAnimationSpec = animationSpec;
        var containerPaddings = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>(MutableStateOf(new Vector2(-1, -1)));
        var contentSize = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>>(MutableStateOf(new Vector2(-1, -1)));
        var contentStyle = Modifier;
        var containerStyle = Modifier.Clip().OnLocallyPositioned(!__composer.Changed(containerPaddings) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it =>
        {
            containerPaddings.Value = new Vector2(it.PaddingLeft + it.PaddingRight, it.PaddingTop + it.PaddingBottom).Approximate();
        }));
        if (!IsInPreview)
        {
            contentStyle = contentStyle.OnLocallyPositioned(!__composer.Changed(contentSize) ? __composer.RememberedValue<System.Action<UnityCompose.LayoutCoordinates>>() : __composer.UpdateRememberedValue<System.Action<UnityCompose.LayoutCoordinates>>(it =>
            {
                var resolvedSize = it.SizeWithPaddings;
                resolvedSize += Vector2.right * (it.MarginLeft + it.MarginRight);
                resolvedSize += Vector2.up * (it.MarginTop + it.MarginBottom);
                contentSize.Value = resolvedSize.Approximate();
            }));
            var isSizeValid = contentSize.Value is { x: > 0, y: > 0 } && containerPaddings.Value is { x: >= 0, y: >= 0 };
            if (isSizeValid)
            {
                var animatedSize = key != null ? AnimateVector2AsState(key: key, targetValueFactory: !__composer.ChangedAsStruct((containerPaddings, contentSize)) ? __composer.RememberedValue<System.Func<UnityEngine.Vector2>>() : __composer.UpdateRememberedValue<System.Func<UnityEngine.Vector2>>(() => contentSize.Value + containerPaddings.Value), animationSpec: resolvedAnimationSpec).Value : AnimateVector2AsState(targetValue: contentSize.Value + containerPaddings.Value, animationSpec: resolvedAnimationSpec).Value;
                containerStyle = containerStyle.Size(width: animatedSize.x, height: animatedSize.y);
                contentStyle = contentStyle.Float();
            }
        }

        return (containerStyle, contentStyle);
    }
}