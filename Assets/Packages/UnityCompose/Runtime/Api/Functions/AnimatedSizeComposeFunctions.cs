using System.Diagnostics.CodeAnalysis;
using SharpExtensions;
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
        ComposableContent<IModifier> content,
        IModifier? modifier = null,
        Optional<AnimationSpec> animationSpec = default
    )
    {
        var resolvedAnimationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
        var (containerModifier, contentModifier) = AnimateSizeModifiers(resolvedAnimationSpec.GetOrDefault());

        ReusableComposeView<AnimatedSize>(
            modifier: modifier.OrEmpty()
                .Then(containerModifier),
            initializer: it =>
            {
                it.style.alignItems = Align.Center;
                it.style.justifyContent = Justify.Center;
            },
            content: () =>
            {
                content(contentModifier);
            }
        );
    }

    [Composable]
    private static (IModifier ContainerModifier, IModifier ContentModifier) AnimateSizeModifiers(
        AnimationSpec animationSpec,
        object? key = null
    )
    {
        var containerPaddings = Remember(() => MutableStateOf(new Vector2(-1, -1)));
        var contentSize = Remember(() => MutableStateOf(new Vector2(-1, -1)));
        var contentModifier = Modifier;
        var containerModifier = Modifier
            .Clip()
            .OnLocallyPositioned(it =>
            {
                containerPaddings.Value = new Vector2(
                    it.PaddingLeft + it.PaddingRight,
                    it.PaddingTop + it.PaddingBottom
                ).Approximate();
            });

        if (!IsInPreview)
        {
            contentModifier = contentModifier
                .OnLocallyPositioned(it =>
                {
                    var resolvedSize = it.SizeWithPaddings;
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
                        animationSpec: animationSpec
                    ).Value
                    : AnimateVector2AsState(
                        targetValue: contentSize.Value + containerPaddings.Value,
                        animationSpec: animationSpec
                    ).Value;
                containerModifier = containerModifier
                    .Size(width: animatedSize.x.Px(), height: animatedSize.y.Px());
                contentModifier = contentModifier
                    .Float();
            }
        }

        return (containerModifier, contentModifier);
    }
}