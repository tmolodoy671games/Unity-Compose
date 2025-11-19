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
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [Composable]
    private static void __AnimatedSize(Action content, IModifier? modifier = null, Optional<AnimationSpec> animationSpec = default)
    {
        var(__content, __modifier, __animationSpec) = (content, modifier, animationSpec);
        if (CurrentComposer.BeginComposeGroup((__content, __modifier, __animationSpec)))
            return;
        try
        {
            var resolvedAnimationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
            var(containerStyle, contentStyle) = AnimateSizeModifiers(resolvedAnimationSpec.GetOrDefault());
            ReusableComposeView<AnimatedSize>(modifier: modifier.OrEmpty().Then(containerStyle), initializer: CurrentComposer.WithState(string.Empty).Remember<System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.AnimatedSize>?>(__ => it =>
            {
                it.style.alignItems = Align.Center;
                it.style.justifyContent = Justify.Center;
            }), content: CurrentComposer.WithState((content, contentStyle)).Remember<System.Action?>(__ => () =>
            {
                CompositionLocalProvider(LocalModifier.Provides(after: contentStyle), content: content);
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__content, __modifier, __animationSpec)).Remember<Action>(__ => () => __AnimatedSize(__.__content, __.__modifier, __.__animationSpec)));
        }
    }

    [Composable]
    private static (IModifier ContainerModifier, IModifier ContentModifier) __AnimateSizeModifiers(AnimationSpec animationSpec, object? key = null)
    {
        var resolvedAnimationSpec = animationSpec;
        var containerPaddings = Remember(() => MutableStateOf(new Vector2(-1, -1)));
        var contentSize = Remember(() => MutableStateOf(new Vector2(-1, -1)));
        var contentStyle = Modifier;
        var containerStyle = Modifier.Clip().OnLocallyPositioned(CurrentComposer.WithState(containerPaddings).Remember<System.Action<UnityCompose.LayoutCoordinates>>(__ => it =>
        {
            containerPaddings.Value = new Vector2(it.PaddingLeft + it.PaddingRight, it.PaddingTop + it.PaddingBottom).Approximate();
        }));
        if (!IsInPreview)
        {
            contentStyle = contentStyle.OnLocallyPositioned(CurrentComposer.WithState(contentSize).Remember<System.Action<UnityCompose.LayoutCoordinates>>(__ => it =>
            {
                var resolvedSize = it.SizeWithPaddings;
                resolvedSize += Vector2.right * (it.MarginLeft + it.MarginRight);
                resolvedSize += Vector2.up * (it.MarginTop + it.MarginBottom);
                contentSize.Value = resolvedSize.Approximate();
            }));
            var isSizeValid = contentSize.Value is { x: > 0, y: > 0 } && containerPaddings.Value is { x: >= 0, y: >= 0 };
            if (isSizeValid)
            {
                var animatedSize = key != null ? AnimateVector2AsState(key: key, targetValueFactory: CurrentComposer.WithState((containerPaddings, contentSize)).Remember<System.Func<UnityEngine.Vector2>>(__ => () => contentSize.Value + containerPaddings.Value), animationSpec: resolvedAnimationSpec).Value : AnimateVector2AsState(targetValue: contentSize.Value + containerPaddings.Value, animationSpec: resolvedAnimationSpec).Value;
                containerStyle = containerStyle.Size(width: animatedSize.x, height: animatedSize.y);
                contentStyle = contentStyle.Float();
            }
        }

        return (containerStyle, contentStyle);
    }
}