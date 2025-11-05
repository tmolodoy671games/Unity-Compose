using System;
using Microsoft.CodeAnalysis;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    [Compiled]
    private static void __AnimatedContent<T>(T targetState, Func<IAnimatedContentTransitionScope<T>, ContentTransform> transitionSpec, [Composable] Action<T> content, Optional<AnimationSpec> sizeAnimationSpec = default, IModifier? modifier = null)
    {
        if (CurrentComposer.BeginComposeGroup((targetState, transitionSpec, content, sizeAnimationSpec, modifier)))
            return;
        try
        {
            // Progress:
            var isSwitched = Remember(() => MutableStateOf(false));
            LaunchedEffect(targetState!, Remember<global::System.Action>(isSwitched, () => isSwitched.Value = !isSwitched.Value));
            var previousValue = Remember(() => IMutableStableProperty.Create(targetState));
            var targetValue = Remember(() => IMutableStableProperty.Create(targetState));
            LaunchedEffect(targetState!, Remember<global::System.Action>((targetState, previousValue, targetValue), () =>
            {
                previousValue.Value = targetValue.Value;
                targetValue.Value = targetState;
            }));
            var resolvedTransition = Remember(targetState!, () => Equals(previousValue.Value, targetState) ? IEnterTransition.Empty().TogetherWith(Hide()) : transitionSpec(new AnimatedContentTransitionScopeImpl<T>(previousValue.Value, targetState)));
            var transitionDuration = resolvedTransition.TotalDuration;
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: transitionDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var resolvedTimeElapsed = resolvedProgress * transitionDuration;
            // Animating size:
            var(containerModifier, contentModifier) = sizeAnimationSpec.HasValue ? AnimateSizeModifiers(sizeAnimationSpec.Value) : (Modifier, Modifier);
            // Layout:
            ReusableComposeView<AnimatedContent>(modifier: modifier.OrEmpty().Then(containerModifier), content: RememberComposable<global::System.Action>((targetState, content, isSwitched, previousValue, resolvedTransition, resolvedProgress, resolvedTimeElapsed, contentModifier), () =>
            {
                var parent = LocalParentLayout.Current;
                var nextModifier = resolvedTransition.Enter.Get(resolvedTimeElapsed, parent).Then(contentModifier);
                var previousModifier = resolvedTransition.Exit.Get(resolvedTimeElapsed, parent).Float();
                var isAnimationRunning = resolvedProgress is> 0 and < 1;
                var next = (Value: targetState, Style: nextModifier, ContentState: isAnimationRunning ? ContentState.Idle : ContentState.Entering);
                var previous = (Value: previousValue.Value, Style: previousModifier, ContentState: ContentState.Exiting);
                var pair = isSwitched.Value ? (First: next, Second: previous) : (First: previous, Second: next);
                if (isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "First", content: RememberComposable<global::System.Action>((content, resolvedTransition, resolvedProgress, pair), () =>
                    {
                        CompositionLocalProvider(provides: IImmutableStableList.Create(LocalModifier.Provides(after: pair.First.Style), LocalTransitionState.Provides(TransitionState.Create(state: pair.First.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration))), content: RememberComposable<global::System.Action>((content, pair), () => content(pair.First.Value)));
                    }));
                }

                if (!isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "Second", content: RememberComposable<global::System.Action>((content, resolvedTransition, resolvedProgress, pair), () =>
                    {
                        CompositionLocalProvider(provides: IImmutableStableList.Create(LocalModifier.Provides(after: pair.Second.Style), LocalTransitionState.Provides(TransitionState.Create(state: pair.First.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration))), content: RememberComposable<global::System.Action>((content, pair), () => content(pair.Second.Value)));
                    }));
                }
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __AnimatedContent<T>(targetState, transitionSpec, content, sizeAnimationSpec, modifier));
        }
    }
}