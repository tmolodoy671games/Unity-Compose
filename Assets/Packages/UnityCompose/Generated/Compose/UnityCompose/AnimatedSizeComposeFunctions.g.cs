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
    private static void __AnimatedSize(ComposableContent content, IModifier? modifier = null, Optional<AnimationSpec> animationSpec = default)
    {
        var(__content, __modifier, __animationSpec) = (content, modifier, animationSpec);
        if (CurrentComposer.BeginComposeGroup(1826694553, (__content, __modifier, __animationSpec)))
            return;
        try
        {
            var resolvedAnimationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
            var(containerStyle, contentStyle) = AnimateSizeModifiers(resolvedAnimationSpec.GetOrDefault());
            ReusableComposeView<AnimatedSize>(modifier: modifier.OrEmpty().Then(containerStyle), initializer: CurrentComposer.HasRememberedValue<bool, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.AnimatedSize>?>(640412447, true) ? CurrentComposer.RememberedValue<bool, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.AnimatedSize>?>() : CurrentComposer.WriteLambda<bool, System.Action<UnityCompose.Packages.UnityCompose.Runtime.Impl.Views.AnimatedSize>?>(it =>
            {
                it.style.alignItems = Align.Center;
                it.style.justifyContent = Justify.Center;
            }), content: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.ComposableContent, UnityCompose.IModifier?>, UnityCompose.ComposableContent?>(228214124, (content, contentStyle)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.ComposableContent, UnityCompose.IModifier?>, UnityCompose.ComposableContent?>() : CurrentComposer.WriteComposableLambda<ValueTuple<UnityCompose.ComposableContent, UnityCompose.IModifier?>, UnityCompose.ComposableContent?>(() =>
            {
                CompositionLocalProvider(LocalModifier.Provides(after: contentStyle), content: content);
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<ComposableContent, IModifier?, Optional<AnimationSpec>>, Action>(1826794553, (__content, __modifier, __animationSpec)) ? CurrentComposer.RememberedValue<ValueTuple<ComposableContent, IModifier?, Optional<AnimationSpec>>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<ComposableContent, IModifier?, Optional<AnimationSpec>>, Action>(() => __AnimatedSize(__content, __modifier, __animationSpec)));
        }
    }

    [Composable]
    private static (IModifier ContainerModifier, IModifier ContentModifier) __AnimateSizeModifiers(AnimationSpec animationSpec, object? key = null)
    {
        var resolvedAnimationSpec = animationSpec;
        var containerPaddings = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<UnityEngine.Vector2>>(329449041, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<UnityEngine.Vector2>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<UnityEngine.Vector2>>(() => MutableStateOf(new Vector2(-1, -1)));
        var contentSize = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<UnityEngine.Vector2>>(1301468995, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<UnityEngine.Vector2>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<UnityEngine.Vector2>>(() => MutableStateOf(new Vector2(-1, -1)));
        var contentStyle = Modifier;
        var containerStyle = Modifier.Clip().OnLocallyPositioned(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(1752642721, containerPaddings) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(it =>
        {
            containerPaddings.Value = new Vector2(it.PaddingLeft + it.PaddingRight, it.PaddingTop + it.PaddingBottom).Approximate();
        }));
        if (!IsInPreview)
        {
            contentStyle = contentStyle.OnLocallyPositioned(CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(-1662389811, contentSize) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<UnityEngine.Vector2>?, System.Action<UnityCompose.LayoutCoordinates>>(it =>
            {
                var resolvedSize = it.SizeWithPaddings;
                resolvedSize += Vector2.right * (it.MarginLeft + it.MarginRight);
                resolvedSize += Vector2.up * (it.MarginTop + it.MarginBottom);
                contentSize.Value = resolvedSize.Approximate();
            }));
            var isSizeValid = contentSize.Value is { x: > 0, y: > 0 } && containerPaddings.Value is { x: >= 0, y: >= 0 };
            if (isSizeValid)
            {
                var animatedSize = key != null ? AnimateVector2AsState(key: key, targetValueFactory: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.IMutableState<UnityEngine.Vector2>?, UnityCompose.IMutableState<UnityEngine.Vector2>?>, System.Func<UnityEngine.Vector2>>(-1914140351, (containerPaddings, contentSize)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.IMutableState<UnityEngine.Vector2>?, UnityCompose.IMutableState<UnityEngine.Vector2>?>, System.Func<UnityEngine.Vector2>>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.IMutableState<UnityEngine.Vector2>?, UnityCompose.IMutableState<UnityEngine.Vector2>?>, System.Func<UnityEngine.Vector2>>(() => contentSize.Value + containerPaddings.Value), animationSpec: resolvedAnimationSpec).Value : AnimateVector2AsState(targetValue: contentSize.Value + containerPaddings.Value, animationSpec: resolvedAnimationSpec).Value;
                containerStyle = containerStyle.Size(width: animatedSize.x, height: animatedSize.y);
                contentStyle = contentStyle.Float();
            }
        }

        return (containerStyle, contentStyle);
    }
}