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
        CompositionLocalOf(() => TransitionState.Create(TransitionPhase.Idle, 0, 0));
    public static readonly ICompositionLocal<TransitionState> LocalResolvedTransitionState =
        CompositionLocalOf(() => TransitionState.Create(TransitionPhase.Idle, 0, 0));

    [Composable]
    public static void AnimatedContent<T>(
        T targetState,
        Func<IAnimatedContentTransitionScope<T>, ContentTransform> transitionSpec,
        ComposableContent<T, IModifier> content,
        Optional<AnimationSpec> sizeAnimationSpec = default,
        IModifier? modifier = null
    )
    {
        // Progress:
        var isSwitched = Remember(() => MutableStateOf(false));
        SideEffect(targetState, () => isSwitched.Value = !isSwitched.Value);

        var previousValue = Remember(() => IMutableStableProperty.Create(targetState));
        var targetValue = Remember(() => IMutableStableProperty.Create(targetState));
        SideEffect(targetState, () =>
        {
            previousValue.Value = targetValue.Value;
            targetValue.Value = targetState;
        });

        var resolvedTransition = Remember(
            targetState,
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
                    Modifier: nextModifier,
                    ContentState: isAnimationRunning ? TransitionPhase.Idle : TransitionPhase.Entering
                );
                var previous = (
                    Value: previousValue.Value,
                    Modifier: previousModifier,
                    ContentState: TransitionPhase.Exiting
                );
                var pair = isSwitched.Value
                    ? (First: next, Second: previous)
                    : (First: previous, Second: next);

                if (isSwitched.Value || isAnimationRunning)
                {
                    var state = TransitionState.Create(
                        phase: pair.First.ContentState,
                        absoluteProgress: resolvedProgress,
                        duration: resolvedTransition.TotalDuration
                    );
                    var resolvedState = TransitionState.Create(state, LocalResolvedTransitionState.Current);
                    CompositionLocalProvider(
                        LocalTransitionState.Provides(state),
                        LocalResolvedTransitionState.Provides(resolvedState),
                        content: () => content(pair.First.Value, pair.First.Modifier)
                    );
                }

                if (!isSwitched.Value || isAnimationRunning)
                {
                    var state = TransitionState.Create(
                        phase: pair.Second.ContentState,
                        absoluteProgress: resolvedProgress,
                        duration: resolvedTransition.TotalDuration
                    );
                    var resolvedState = TransitionState.Create(state, LocalResolvedTransitionState.Current);
                    CompositionLocalProvider(
                        LocalTransitionState.Provides(state),
                        LocalResolvedTransitionState.Provides(resolvedState),
                        content: () => content(pair.Second.Value, pair.Second.Modifier)
                    );
                }
            }
        );
    }
}