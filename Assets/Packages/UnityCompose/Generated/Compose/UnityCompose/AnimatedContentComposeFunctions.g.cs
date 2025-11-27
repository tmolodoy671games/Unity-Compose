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
        if (CurrentComposer.BeginComposeGroup(497003188, (__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier)))
            return;
        try
        {
            // Progress:
            var isSwitched = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(-319971692, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => MutableStateOf(false));
            LaunchedEffect(targetState!, CurrentComposer.HasRememberedValue<UnityCompose.IMutableState<bool>?, System.Action>(-79439535, isSwitched) ? CurrentComposer.RememberedValue<UnityCompose.IMutableState<bool>?, System.Action>() : CurrentComposer.WriteLambda<UnityCompose.IMutableState<bool>?, System.Action>(() => isSwitched.Value = !isSwitched.Value));
            var previousValue = CurrentComposer.HasRememberedValue<bool, StableCollections.IMutableStableProperty<T>>(1509581626, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IMutableStableProperty<T>>() : CurrentComposer.WriteValue<bool, StableCollections.IMutableStableProperty<T>>(() => IMutableStableProperty.Create(targetState));
            var targetValue = CurrentComposer.HasRememberedValue<bool, StableCollections.IMutableStableProperty<T>>(-1019473147, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IMutableStableProperty<T>>() : CurrentComposer.WriteValue<bool, StableCollections.IMutableStableProperty<T>>(() => IMutableStableProperty.Create(targetState));
            LaunchedEffect(targetState!, CurrentComposer.HasRememberedValue<ValueTuple<T, StableCollections.IMutableStableProperty<T>?, StableCollections.IMutableStableProperty<T>?>, System.Action>(1126879073, (targetState, previousValue, targetValue)) ? CurrentComposer.RememberedValue<ValueTuple<T, StableCollections.IMutableStableProperty<T>?, StableCollections.IMutableStableProperty<T>?>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<T, StableCollections.IMutableStableProperty<T>?, StableCollections.IMutableStableProperty<T>?>, System.Action>(() =>
            {
                previousValue.Value = targetValue.Value;
                targetValue.Value = targetState;
            }));
            var resolvedTransition = CurrentComposer.HasRememberedValue<T, UnityCompose.ContentTransform>(388032863, targetState!) ? CurrentComposer.RememberedValue<T, UnityCompose.ContentTransform>() : CurrentComposer.WriteValue<T, UnityCompose.ContentTransform>(() => Equals(previousValue.Value, targetState) ? IEnterTransition.Empty().TogetherWith(Hide()) : transitionSpec(new AnimatedContentTransitionScopeImpl<T>(previousValue.Value, targetState)));
            var transitionDuration = resolvedTransition.TotalDuration;
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: transitionDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var resolvedTimeElapsed = resolvedProgress * transitionDuration;
            // Animating size:
            var(containerModifier, contentModifier) = sizeAnimationSpec.HasValue ? AnimateSizeModifiers(sizeAnimationSpec.Value, key: targetState) : (Modifier, Modifier);
            // Layout:
            ReusableComposeView<AnimatedContent>(modifier: modifier.OrEmpty().Then(containerModifier), content: CurrentComposer.HasRememberedValue<ValueTuple<T, UnityCompose.ComposableContent<T>, UnityCompose.IMutableState<bool>?, StableCollections.IMutableStableProperty<T>?, UnityCompose.ContentTransform, float, float, ValueTuple<UnityCompose.IModifier?>>, UnityCompose.ComposableContent?>(-1872792463, (targetState, content, isSwitched, previousValue, resolvedTransition, resolvedProgress, resolvedTimeElapsed, contentModifier)) ? CurrentComposer.RememberedValue<ValueTuple<T, UnityCompose.ComposableContent<T>, UnityCompose.IMutableState<bool>?, StableCollections.IMutableStableProperty<T>?, UnityCompose.ContentTransform, float, float, ValueTuple<UnityCompose.IModifier?>>, UnityCompose.ComposableContent?>() : CurrentComposer.WriteComposableLambda<ValueTuple<T, UnityCompose.ComposableContent<T>, UnityCompose.IMutableState<bool>?, StableCollections.IMutableStableProperty<T>?, UnityCompose.ContentTransform, float, float, ValueTuple<UnityCompose.IModifier?>>, UnityCompose.ComposableContent?>(() =>
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
                    Key(key: "First", content: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.ComposableContent<T>, UnityCompose.ContentTransform, float, ((T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) First, (T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) Second)>, System.Action>(1563832431, (content, resolvedTransition, resolvedProgress, pair)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.ComposableContent<T>, UnityCompose.ContentTransform, float, ((T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) First, (T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) Second)>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.ComposableContent<T>, UnityCompose.ContentTransform, float, ((T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) First, (T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) Second)>, System.Action>(() =>
                    {
                        var state = TransitionResolvedState.Create(state: pair.First.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        CompositionLocalProvider(LocalModifier.Provides(after: pair.First.Style), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: () => content(pair.First.Value));
                    }));
                }

                if (!isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "Second", content: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.ComposableContent<T>, UnityCompose.ContentTransform, float, ((T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) First, (T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) Second)>, System.Action>(1682617769, (content, resolvedTransition, resolvedProgress, pair)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.ComposableContent<T>, UnityCompose.ContentTransform, float, ((T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) First, (T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) Second)>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.ComposableContent<T>, UnityCompose.ContentTransform, float, ((T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) First, (T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) Second)>, System.Action>(() =>
                    {
                        var state = TransitionResolvedState.Create(state: pair.Second.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        CompositionLocalProvider(LocalModifier.Provides(after: pair.Second.Style), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: () => content(pair.Second.Value));
                    }));
                }
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<T, Func<IAnimatedContentTransitionScope<T>, ContentTransform>, ComposableContent<T>, Optional<AnimationSpec>, IModifier?>, Action>(497103188, (__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier)) ? CurrentComposer.RememberedValue<ValueTuple<T, Func<IAnimatedContentTransitionScope<T>, ContentTransform>, ComposableContent<T>, Optional<AnimationSpec>, IModifier?>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<T, Func<IAnimatedContentTransitionScope<T>, ContentTransform>, ComposableContent<T>, Optional<AnimationSpec>, IModifier?>, Action>(() => __AnimatedContent(__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier)));
        }
    }
}