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
    private static void __AnimatedContent<T>(T targetState, Func<IAnimatedContentTransitionScope<T>, ContentTransform> transitionSpec, ComposableContent<T> content, Optional<AnimationSpec> sizeAnimationSpec = default, IModifier? modifier = null)
    {
        var(__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier) = (targetState, transitionSpec, content, sizeAnimationSpec, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-1433093757);
        if (__composer.ShouldExecuteAsStruct((__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier)))
        {
            var isSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
            LaunchedEffect(targetState!, !__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value));
            var previousValue = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<T>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<T>>(IMutableStableProperty.Create(targetState));
            var targetValue = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<T>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<T>>(IMutableStableProperty.Create(targetState));
            LaunchedEffect(targetState!, !__composer.ChangedAsStruct((targetState, previousValue, targetValue)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
            {
                previousValue.Value = targetValue.Value;
                targetValue.Value = targetState;
            }));
            var resolvedTransition = !__composer.Changed(targetState!) ? __composer.RememberedValueAsStruct<UnityCompose.ContentTransform>() : __composer.UpdateRememberedValue<UnityCompose.ContentTransform>(Equals(previousValue.Value, targetState) ? IEnterTransition.Empty().TogetherWith(Hide()) : transitionSpec(new AnimatedContentTransitionScopeImpl<T>(previousValue.Value, targetState)));
            var transitionDuration = resolvedTransition.TotalDuration;
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: transitionDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var resolvedTimeElapsed = resolvedProgress * transitionDuration;
            var(containerModifier, contentModifier) = sizeAnimationSpec.HasValue ? AnimateSizeModifiers(sizeAnimationSpec.Value, key: targetState) : (Modifier, Modifier);
            ReusableComposeView<AnimatedContent>(modifier: modifier.OrEmpty().Then(containerModifier), content: !__composer.ChangedAsStruct((targetState, content, isSwitched, previousValue, resolvedTransition, resolvedProgress, resolvedTimeElapsed, contentModifier)) ? __composer.RememberedValue<UnityCompose.ComposableContent?>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent?>(() =>
            {
                var parent = LocalVisualElement.Current;
                var nextModifier = resolvedTransition.Enter.Get(resolvedTimeElapsed, parent).Then(contentModifier);
                var previousModifier = resolvedTransition.Exit.Get(resolvedTimeElapsed, parent).Float();
                var isAnimationRunning = resolvedProgress is> 0 and < 1;
                var next = (Value: targetState, Style: nextModifier, ContentState: isAnimationRunning ? TransitionState.Idle : TransitionState.Entering);
                var previous = (Value: previousValue.Value, Style: previousModifier, ContentState: TransitionState.Exiting);
                var pair = isSwitched.Value ? (First: next, Second: previous) : (First: previous, Second: next);
                __composer.StartReplaceGroup(6755691);
                if (isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "First", content: !__composer.ChangedAsStruct((content, resolvedTransition, resolvedProgress, pair)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                    {
                        var state = TransitionResolvedState.Create(state: pair.First.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        CompositionLocalProvider(LocalModifier.Provides(after: pair.First.Style), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: () => content(pair.First.Value));
                    }));
                }

                __composer.EndReplaceGroup(6755691);
                __composer.StartReplaceGroup(1194410071);
                if (!isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "Second", content: !__composer.ChangedAsStruct((content, resolvedTransition, resolvedProgress, pair)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
                    {
                        var state = TransitionResolvedState.Create(state: pair.Second.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        CompositionLocalProvider(LocalModifier.Provides(after: pair.Second.Style), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: () => content(pair.Second.Value));
                    }));
                }

                __composer.EndReplaceGroup(1194410071);
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-1433093757)?.UpdateScope(() => __AnimatedContent(__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier));
    }
}