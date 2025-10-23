using System;
using System.Diagnostics.CodeAnalysis;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    [SuppressMessage("ReSharper", "ExplicitCallerInfoArgument")]
    [Composable]
    public static void AnimatedSize(
        Action content,
        IModifier? modifier = null,
        float duration = ComposeDefaults.TransitionDuration
    )
    {
        var (containerStyle, contentStyle) = AnimateSizeModifiers(duration);

        ReusableComposeView<AnimatedSize>(
            modifier: modifier.OrEmpty()
                .Then(containerStyle),
            content: () =>
            {
                CompositionLocalProvider(
                    provides: IImmutableStableList.Create(
                        LocalModifier.Provides(after: contentStyle)
                    ),
                    content: content
                );
            }
        );
    }

    [Composable]
    private static (IModifier ContainerModifier, IModifier ContentModifier) AnimateSizeModifiers(
        float duration,
        object? key = null
    )
    {
        var containerPaddings = Remember(() => MutableStateOf(new Vector2(-1, -1)));
        var contentSize = Remember(() => MutableStateOf(new Vector2(-1, -1)));
        var contentStyle = Modifier;
        var containerStyle = Modifier
            .Clip()
            .OnGloballyPositioned(it =>
            {
                containerPaddings.Value = new Vector2(
                    it.PaddingLeft + it.PaddingRight,
                    it.PaddingTop + it.PaddingBottom
                ).Approximate();
            });

        if (!IsInPreview)
        {
            contentStyle = contentStyle
                .OnGloballyPositioned(it =>
                {
                    var resolvedSize = it.Size;
                    resolvedSize += Vector2.right * (it.MarginLeft + it.MarginRight);
                    resolvedSize += Vector2.up * (it.MarginTop + it.MarginBottom);
                    contentSize.Value = resolvedSize.Approximate();
                });
            var isSizeValid = contentSize.Value is { x: > 0, y: > 0 } &&
                              containerPaddings.Value is { x: >= 0, y: >= 0 };
            if (isSizeValid)
            {
                var animatedSize = key != null
                    ? AnimateVector2AsState(
                        key: key,
                        targetValueFactory: () => contentSize.Value + containerPaddings.Value,
                        duration: duration
                    ).Value
                    : AnimateVector2AsState(
                        targetValue: contentSize.Value + containerPaddings.Value,
                        duration: duration
                    ).Value;
                containerStyle = containerStyle
                    .Size(animatedSize);
                contentStyle = contentStyle
                    .Float();
            }
        }

        return (containerStyle, contentStyle);
    }
}