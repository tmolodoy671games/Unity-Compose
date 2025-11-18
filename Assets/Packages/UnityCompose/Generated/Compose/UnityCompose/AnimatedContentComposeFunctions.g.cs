using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using SharpExtensions;
using UnityEngine;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __AnimatedContent<T>(T targetState, Func<IAnimatedContentTransitionScope<T>, ContentTransform> transitionSpec, [Composable] Action<T> content, Optional<AnimationSpec> sizeAnimationSpec = default, IModifier? modifier = null)
    {
        var(__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier) = (targetState, transitionSpec, content, sizeAnimationSpec, modifier);
        if (CurrentComposer.BeginComposeGroup((__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier)))
            return;
        try
        {
            // Progress:
            var isSwitched = Remember(CurrentComposer.WithState(string.Empty).Remember<System.Func<UnityCompose.IMutableState<bool>>>(__ => () => MutableStateOf(false)));
            LaunchedEffect(targetState!, CurrentComposer.WithState(isSwitched).Remember<System.Action>(__ => () => isSwitched.Value = !isSwitched.Value));
            var previousValue = Remember(CurrentComposer.WithState(targetState).Remember<System.Func<StableCollections.IMutableStableProperty<T>>>(__ => () => IMutableStableProperty.Create(targetState)));
            var targetValue = Remember(CurrentComposer.WithState(targetState).Remember<System.Func<StableCollections.IMutableStableProperty<T>>>(__ => () => IMutableStableProperty.Create(targetState)));
            LaunchedEffect(targetState!, CurrentComposer.WithState((targetState, previousValue, targetValue)).Remember<System.Action>(__ => () =>
            {
                previousValue.Value = targetValue.Value;
                targetValue.Value = targetState;
            }));
            var resolvedTransition = Remember(targetState!, CurrentComposer.WithState((targetState, transitionSpec, previousValue)).Remember<System.Func<UnityCompose.ContentTransform>>(__ => () => Equals(previousValue.Value, targetState) ? IEnterTransition.Empty().TogetherWith(Hide()) : transitionSpec(new AnimatedContentTransitionScopeImpl<T>(previousValue.Value, targetState))));
            var transitionDuration = resolvedTransition.TotalDuration;
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: transitionDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var resolvedTimeElapsed = resolvedProgress * transitionDuration;
            // Animating size:
            var(containerModifier, contentModifier) = sizeAnimationSpec.HasValue ? AnimateSizeModifiers(sizeAnimationSpec.Value, key: targetState) : (Modifier, Modifier);
            // Layout:
            ReusableComposeView<AnimatedContent>(modifier: modifier.OrEmpty().Then(containerModifier), content: CurrentComposer.WithState((targetState, content, isSwitched, previousValue, resolvedTransition, resolvedProgress, resolvedTimeElapsed, contentModifier)).Remember<System.Action?>(__ => () =>
            {
                var parent = LocalVisualElement.Current;
                var nextModifier = resolvedTransition.Enter.Get(resolvedTimeElapsed, parent).Then(contentModifier);
                var previousModifier = resolvedTransition.Exit.Get(resolvedTimeElapsed, parent).Float();
                var isAnimationRunning = resolvedProgress is> 0 and < 1;
                var next = (Value: targetState, Style: nextModifier, ContentState: isAnimationRunning ? TransitionState.Idle : TransitionState.Entering);
                var previous = (Value: previousValue.Value, Style: previousModifier, ContentState: TransitionState.Exiting);
                var pair = isSwitched.Value ? (First: next, Second: previous) : (First: previous, Second: next);
                if (isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "First", content: CurrentComposer.WithState((content, resolvedTransition, resolvedProgress, pair)).Remember<System.Action>(__ => () =>
                    {
                        var state = TransitionResolvedState.Create(state: pair.First.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        CompositionLocalProvider(LocalModifier.Provides(after: pair.First.Style), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: CurrentComposer.WithState((content, pair)).Remember<System.Action>(__ => () => content(pair.First.Value)));
                    }));
                }

                if (!isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "Second", content: CurrentComposer.WithState((content, resolvedTransition, resolvedProgress, pair)).Remember<System.Action>(__ => () =>
                    {
                        var state = TransitionResolvedState.Create(state: pair.Second.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        CompositionLocalProvider(LocalModifier.Provides(after: pair.Second.Style), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: CurrentComposer.WithState((content, pair)).Remember<System.Action>(__ => () => content(pair.Second.Value)));
                    }));
                }
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier)).Remember<Action>(__ => () => __AnimatedContent(__.__targetState, __.__transitionSpec, __.__content, __.__sizeAnimationSpec, __.__modifier)));
        }
    }
}