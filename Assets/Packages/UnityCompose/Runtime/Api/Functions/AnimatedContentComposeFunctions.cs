using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static readonly ICompositionLocal<float> LocalTransitionProgress = CompositionLocalOf(() => 1f);
    public static readonly ICompositionLocal<float> LocalTransitionTimeElapsed = CompositionLocalOf(() => 0f);

    public static readonly ICompositionLocal<ContentState> LocalContentState =
        CompositionLocalOf(() => ContentState.Idle);

    public static ContentTransform InstantContentTransform => UnityCompose.ContentTransform.Instant;

    [Composable]
    public static void AnimatedContent<T>(
        T targetState,
        Func<IAnimatedContentTransitionScope<T>, ContentTransform> transitionSpec,
        [Composable] Action<T> content,
        AnimationSpec sizeAnimationSpec = default,
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
                ? IEnterTransition.Empty.TogetherWith(Hide())
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
            ? AnimateSizeModifiers(sizeAnimationSpec)
            : (Modifier, Modifier);

        // Layout:
        ReusableComposeView<AnimatedContent>(
            modifier: modifier.OrEmpty()
                .Then(containerModifier),
            content: () =>
            {
                var parent = LocalParentLayout.Current;
                var nextModifier = resolvedTransition.Enter.Get(resolvedTimeElapsed, parent)
                    .Then(contentModifier);
                var previousModifier = resolvedTransition.Exit.Get(resolvedTimeElapsed, parent)
                    .Float();
                var isAnimationRunning = resolvedProgress is > 0 and < 1;

                var next = (Value: targetState, Style: nextModifier, Progress: resolvedProgress,
                    ContentState: isAnimationRunning ? ContentState.Idle : ContentState.Entering);
                var previous = (Value: previousValue.Value, Style: previousModifier, Progress: 1 - resolvedProgress,
                    ContentState: ContentState.Exiting);
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
                                    LocalModifier.Provides(after: pair.First.Style),
                                    LocalTransitionProgress.Provides(pair.First.Progress),
                                    LocalTransitionTimeElapsed.Provides(resolvedProgress * transitionDuration),
                                    LocalContentState.Provides(pair.First.ContentState)
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
                                    LocalModifier.Provides(after: pair.Second.Style),
                                    LocalTransitionProgress.Provides(pair.Second.Progress),
                                    LocalTransitionTimeElapsed.Provides(resolvedProgress * transitionDuration),
                                    LocalContentState.Provides(pair.Second.ContentState)
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

public enum ContentState
{
    Entering,
    Idle,
    Exiting,
}

public interface IAnimatedContentTransitionScope<T>
{
    T InitialState { get; }
    T TargetState { get; }
}

internal class AnimatedContentTransitionScopeImpl<T> : IAnimatedContentTransitionScope<T>
{
    public AnimatedContentTransitionScopeImpl(T initialState, T targetState)
    {
        InitialState = initialState;
        TargetState = targetState;
    }

    public T InitialState { get; }
    public T TargetState { get; }
}