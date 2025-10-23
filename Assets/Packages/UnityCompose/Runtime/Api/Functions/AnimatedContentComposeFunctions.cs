using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static readonly ICompositionLocal<float> LocalTransitionProgress = CompositionLocalOf(() => 1f);

    public static ContentTransform InstantContentTransform => UnityCompose.ContentTransform.Instant;

    public static ContentTransform ContentTransform(
        EnterTransition? enter = null,
        ExitTransition? exit = null
    ) => new(enter ?? EnterTransition.EmptyImpl.Instance, exit ?? ExitTransition.EmptyImpl.Instance);

    [Composable]
    public static void AnimatedContent<T>(
        T value,
        Func<T, T, ContentTransform> transition,
        [Composable] Action<T> content,
        bool animateSize = false,
        float transitionDuration = ComposeDefaults.TransitionDuration,
        IModifier? style = null
    )
    {
        // Progress:
        var isSwitched = Remember(() => MutableStateOf(false));
        LaunchedEffect(value!, () => isSwitched.Value = !isSwitched.Value);

        var progress = AnimateFloatAsState(isSwitched.Value ? 1 : 0f, transitionDuration).Value;
        var resolvedProgress = isSwitched.Value ? progress : 1 - progress;

        var previousValue = Remember(() => IMutableStableProperty.Create(value));
        var targetValue = Remember(() => IMutableStableProperty.Create(value));
        LaunchedEffect(value!, () =>
        {
            previousValue.Value = targetValue.Value;
            targetValue.Value = value;
        });

        // Animating size:
        var (containerStyle, contentStyle) = animateSize
            ? AnimateSizeStyles(transitionDuration)
            : (Modifier, Modifier);

        // Layout:
        var resolvedTransition = Remember(
            value!,
            () => Equals(previousValue.Value, value) ? ContentTransform() : transition(previousValue.Value, value)
        );

        ReusableComposeView<AnimatedContent>(
            style: style.OrEmpty()
                .Then(containerStyle),
            content: () =>
            {
                var parent = LocalVisualElement.Current;
                var nextStyle = resolvedTransition.Enter.Get(resolvedProgress, parent)
                    .Then(contentStyle);
                var previousStyle = resolvedTransition.Exit.Get(resolvedProgress, parent)
                    .Float();
                var isAnimationRunning = resolvedProgress is > 0 and < 1;

                var next = (Value: value, Style: nextStyle, Progress: resolvedProgress);
                var previous = (Value: previousValue.Value, Style: previousStyle, Progress: 1 - resolvedProgress);
                var pair = isSwitched.Value
                    ? (First: next, Second: previous)
                    : (First: previous, Second: next);

                if (isSwitched.Value || isAnimationRunning)
                {
                    Key(
                        key: "First",
                        content: () =>
                        {
                            CompositionLocalProvider(
                                provides: IImmutableStableList.Create(
                                    LocalStyle.Provides(after: pair.First.Style),
                                    LocalTransitionProgress.Provides(pair.First.Progress)
                                ),
                                content: () => content(pair.First.Value)
                            );
                        }
                    );
                }

                if (!isSwitched.Value || isAnimationRunning)
                {
                    Key(
                        key: "Second",
                        content: () =>
                        {
                            CompositionLocalProvider(
                                provides: IImmutableStableList.Create(
                                    LocalStyle.Provides(after: pair.Second.Style),
                                    LocalTransitionProgress.Provides(pair.Second.Progress)
                                ),
                                content: () => content(pair.Second.Value)
                            );
                        }
                    );
                }
            }
        );
    }
}