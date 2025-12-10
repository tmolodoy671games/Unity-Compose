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
        __composer.StartRestartGroup(497003188);
        if (__composer.ShouldExecute((__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier)))
        {
            var isSwitched = !__composer.RememberedKeyChanged<bool>(-319971692, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
            LaunchedEffect(targetState!, !__composer.RememberedKeyChanged<UnityCompose.IMutableState<bool>?>(-79439535, isSwitched) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => isSwitched.Value = !isSwitched.Value));
            var previousValue = !__composer.RememberedKeyChanged<bool>(1509581626, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<T>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<T>>(IMutableStableProperty.Create(targetState));
            var targetValue = !__composer.RememberedKeyChanged<bool>(-1019473147, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<T>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<T>>(IMutableStableProperty.Create(targetState));
            LaunchedEffect(targetState!, !__composer.RememberedKeyChanged<ValueTuple<T, StableCollections.IMutableStableProperty<T>?, StableCollections.IMutableStableProperty<T>?>>(1126879073, (targetState, previousValue, targetValue)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
            {
                previousValue.Value = targetValue.Value;
                targetValue.Value = targetState;
            }));
            var resolvedTransition = !__composer.RememberedKeyChanged<T>(388032863, targetState!) ? __composer.RememberedValue<UnityCompose.ContentTransform>() : __composer.UpdateRememberedValue<UnityCompose.ContentTransform>(Equals(previousValue.Value, targetState) ? IEnterTransition.Empty().TogetherWith(Hide()) : transitionSpec(new AnimatedContentTransitionScopeImpl<T>(previousValue.Value, targetState)));
            var transitionDuration = resolvedTransition.TotalDuration;
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: transitionDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var resolvedTimeElapsed = resolvedProgress * transitionDuration;
            var(containerModifier, contentModifier) = sizeAnimationSpec.HasValue ? AnimateSizeModifiers(sizeAnimationSpec.Value, key: targetState) : (Modifier, Modifier);
            ReusableComposeView<AnimatedContent>(modifier: modifier.OrEmpty().Then(containerModifier), content: !__composer.RememberedKeyChanged<ValueTuple<T, UnityCompose.ComposableContent<T>, UnityCompose.IMutableState<bool>?, StableCollections.IMutableStableProperty<T>?, UnityCompose.ContentTransform, float, float, ValueTuple<UnityCompose.IModifier?>>>(-1872792463, (targetState, content, isSwitched, previousValue, resolvedTransition, resolvedProgress, resolvedTimeElapsed, contentModifier)) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent?>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent?>(() =>
            {
                var parent = LocalVisualElement.Current;
                var nextModifier = resolvedTransition.Enter.Get(resolvedTimeElapsed, parent).Then(contentModifier);
                var previousModifier = resolvedTransition.Exit.Get(resolvedTimeElapsed, parent).Float();
                var isAnimationRunning = resolvedProgress is> 0 and < 1;
                var next = (Value: targetState, Style: nextModifier, ContentState: isAnimationRunning ? TransitionState.Idle : TransitionState.Entering);
                var previous = (Value: previousValue.Value, Style: previousModifier, ContentState: TransitionState.Exiting);
                var pair = isSwitched.Value ? (First: next, Second: previous) : (First: previous, Second: next);
                __composer.StartReplaceGroup(-1696347639);
                if (isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "First", content: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.ComposableContent<T>, UnityCompose.ContentTransform, float, ((T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) First, (T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) Second)>>(1563832431, (content, resolvedTransition, resolvedProgress, pair)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
                    {
                        var state = TransitionResolvedState.Create(state: pair.First.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        CompositionLocalProvider(LocalModifier.Provides(after: pair.First.Style), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.ComposableContent<T>, ((T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) First, (T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) Second)>>(1190765668, (content, pair)) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() => content(pair.First.Value)));
                    }));
                }

                __composer.EndReplaceGroup(-1696347639);
                __composer.StartReplaceGroup(1380573395);
                if (!isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "Second", content: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.ComposableContent<T>, UnityCompose.ContentTransform, float, ((T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) First, (T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) Second)>>(1682617769, (content, resolvedTransition, resolvedProgress, pair)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
                    {
                        var state = TransitionResolvedState.Create(state: pair.Second.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        CompositionLocalProvider(LocalModifier.Provides(after: pair.Second.Style), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.ComposableContent<T>, ((T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) First, (T Value, UnityCompose.IModifier Style, UnityCompose.TransitionState ContentState) Second)>>(-1700860922, (content, pair)) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() => content(pair.Second.Value)));
                    }));
                }

                __composer.EndReplaceGroup(1380573395);
            }));
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(497003188)?.UpdateScope(() => __AnimatedContent(__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier));
    }
}