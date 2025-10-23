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
        ComposeStyle? style = null,
        float duration = ComposeDefaults.TransitionDuration
    )
    {
        var (containerStyle, contentStyle) = AnimateSizeStyles(duration);

        ReusableComposeView<AnimatedSize>(
            style: style.OrEmpty()
                .Then(containerStyle),
            content: () =>
            {
                CompositionLocalProvider(
                    provides: IImmutableStableList.Create(
                        LocalStyle.Provides(after: contentStyle)
                    ),
                    content: content
                );
            }
        );
    }

    [Composable]
    private static (ComposeStyle ContainerStyle, ComposeStyle ContentStyle) AnimateSizeStyles(
        float duration,
        object? key = null
    )
    {
        var containerPaddings = Remember(() => MutableStateOf(new Vector2(-1, -1)));
        var contentSize = Remember(() => MutableStateOf(new Vector2(-1, -1)));
        var contentStyle = ComposeStyle.Empty;
        var containerStyle = ComposeStyle.Empty
            .OnGeometryChanged(it =>
            {
                var layout = it.currentTarget.CastTo<VisualElement>().resolvedStyle;
                containerPaddings.Value = new Vector2(
                    layout.paddingLeft + layout.paddingRight,
                    layout.paddingTop + layout.paddingBottom
                ).Approximate();
            });

        if (!IsInPreview)
        {
            contentStyle = contentStyle
                .OnSizeChanged(size => contentSize.Value = size.Approximate());
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
                    .Position(Position.Absolute);
            }
        }

        return (containerStyle, contentStyle);
    }
}