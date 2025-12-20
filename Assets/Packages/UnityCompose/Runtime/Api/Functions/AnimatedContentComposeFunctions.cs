using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using SharpExtensions;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static readonly ICompositionLocal<TransitionState> LocalTransitionState =
        CompositionLocalOf(() => TransitionState.Idle);

    public static readonly ICompositionLocal<float> LocalTransitionProgress = CompositionLocalOf(() => 1f);
    public static readonly ICompositionLocal<float> LocalTransitionAbsoluteProgress = CompositionLocalOf(() => 1f);
    public static readonly ICompositionLocal<float> LocalTransitionAbsoluteTimeElapsed = CompositionLocalOf(() => 0f);
    public static readonly ICompositionLocal<float> LocalTransitionDuration = CompositionLocalOf(() => 0f);

    [Composable]
    public static void AnimatedContent<T>(
        T targetState,
        Func<IAnimatedContentTransitionScope<T>, ContentTransform> transitionSpec,
        ComposableContent<T> content,
        Optional<AnimationSpec> sizeAnimationSpec = default,
        IModifier? modifier = null
    )
    {
        // Progress:
        var isSwitched = Remember(() => MutableStateOf(false));
        LaunchedEffect(targetState!, () => isSwitched.Value = !isSwitched.Value);

        var previousValue = Remember(() => IMutableStableProperty.Create(targetState));
        var targetValue = Remember(() => IMutableStableProperty.Create(targetState));
        LaunchedEffect(targetState!, () =>
        {
            previousValue.Value = targetValue.Value;
            targetValue.Value = targetState;
        });

        var resolvedTransition = Remember(
            targetState!,
            () => Equals(previousValue.Value, targetState)
                ? IEnterTransition.Empty().TogetherWith(Hide())
                : transitionSpec(new AnimatedContentTransitionScopeImpl<T>(previousValue.Value, targetState))
        );
        var transitionDuration = resolvedTransition.TotalDuration;

        var progress = AnimateFloatAsState(
            targetValue: isSwitched.Value ? 1 : 0f,
            animationSpec: Tween(
                easing: LinearEasing,
                duration: transitionDuration
            )
        ).Value;
        var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
        var resolvedTimeElapsed = resolvedProgress * transitionDuration;

        // Animating size:
        var (containerModifier, contentModifier) = sizeAnimationSpec.HasValue
            ? AnimateSizeModifiers(sizeAnimationSpec.Value, key: targetState)
            : (Modifier, Modifier);

        // Layout:
        ReusableComposeView<AnimatedContent>(
            modifier: modifier.OrEmpty()
                .Then(containerModifier),
            content: () =>
            {
                var parent = CurrentComposer.GetParentVisualElement().NotNull();
                var nextModifier = resolvedTransition.Enter.Get(resolvedTimeElapsed, parent)
                    .Then(contentModifier);
                var previousModifier = resolvedTransition.Exit.Get(resolvedTimeElapsed, parent)
                    .Float();
                var isAnimationRunning = resolvedProgress is > 0 and < 1;

                var next = (
                    Value: targetState,
                    Style: nextModifier,
                    ContentState: isAnimationRunning ? TransitionState.Idle : TransitionState.Entering
                );
                var previous = (
                    Value: previousValue.Value,
                    Style: previousModifier,
                    ContentState: TransitionState.Exiting
                );
                var pair = isSwitched.Value
                    ? (First: next, Second: previous)
                    : (First: previous, Second: next);

                if (isSwitched.Value || isAnimationRunning)
                {
                    Key(
                        key: "First",
                        content: () =>
                        {
                            var state = TransitionResolvedState.Create(
                                state: pair.First.ContentState,
                                absoluteProgress: resolvedProgress,
                                duration: resolvedTransition.TotalDuration
                            );
                            CompositionLocalProvider(
                                LocalTransitionState.Provides(state.State),
                                LocalTransitionProgress.Provides(state.Progress),
                                LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress),
                                LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed),
                                LocalTransitionDuration.Provides(state.Duration),
                                content: () =>
                                {
                                    WithModifiers(
                                        after: pair.First.Style,
                                        content: () => content(pair.First.Value)
                                    );
                                }
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
                            var state = TransitionResolvedState.Create(
                                state: pair.Second.ContentState,
                                absoluteProgress: resolvedProgress,
                                duration: resolvedTransition.TotalDuration
                            );
                            CompositionLocalProvider(
                                LocalTransitionState.Provides(state.State),
                                LocalTransitionProgress.Provides(state.Progress),
                                LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress),
                                LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed),
                                LocalTransitionDuration.Provides(state.Duration),
                                content: () =>
                                {
                                    WithModifiers(
                                        after: pair.Second.Style,
                                        content: () => content(pair.Second.Value)
                                    );
                                }
                            );
                        }
                    );
                }
            }
        );
    }
}