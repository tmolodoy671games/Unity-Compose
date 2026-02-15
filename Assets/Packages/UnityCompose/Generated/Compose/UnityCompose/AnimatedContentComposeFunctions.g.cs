#nullable enable
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
    public static void __AnimatedContent<T>(T targetState, Func<IAnimatedContentTransitionScope<T>, ContentTransform> transitionSpec, ComposableContent<T, IModifier> content, Optional<AnimationSpec> sizeAnimationSpec = default, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier) = (targetState, transitionSpec, content, sizeAnimationSpec, modifier);
        var __isCreated = __composer.StartRestartGroup(1366895982);
        var __dirty = __changed;
        var __dirtyRestart = 0;
        if ((__changed & 0b_00_00_00_00_11) == 0)
        {
            __dirty |= __composer.Changed(targetState) ? 0b_00_00_00_00_10 : 0b_00_00_00_00_01;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_00_01;
        }

        if ((__changed & 0b_00_00_00_11_00) == 0)
        {
            __dirty |= __composer.Changed(transitionSpec) ? 0b_00_00_00_10_00 : 0b_00_00_00_01_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_00_01_00;
        }

        if ((__changed & 0b_00_00_11_00_00) == 0)
        {
            __dirty |= __composer.Changed(content) ? 0b_00_00_10_00_00 : 0b_00_00_01_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_00_01_00_00;
        }

        if ((__changed & 0b_00_11_00_00_00) == 0)
        {
            __dirty |= __composer.ChangedAsStruct(sizeAnimationSpec) ? 0b_00_10_00_00_00 : 0b_00_01_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_00_01_00_00_00;
        }

        if ((__changed & 0b_11_00_00_00_00) == 0)
        {
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00 : 0b_01_00_00_00_00;
        }
        else
        {
            __dirtyRestart |= 0b_01_00_00_00_00;
        }

        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01)
        {
            var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
            __LaunchedEffect(targetState, (!__composer.Changed(isSwitched) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isSwitched.Value = !isSwitched.Value)), __composer: __composer, __changed: (__dirty & 0b_00_11));
            var previousValue = (!__composer.Changed() ? __composer.RememberedValue<global::StableCollections.IMutableStableProperty<T>>() : __composer.UpdateRememberedValue<global::StableCollections.IMutableStableProperty<T>>(IMutableStableProperty.Create(targetState)));
            var targetValue = (!__composer.Changed() ? __composer.RememberedValue<global::StableCollections.IMutableStableProperty<T>>() : __composer.UpdateRememberedValue<global::StableCollections.IMutableStableProperty<T>>(IMutableStableProperty.Create(targetState)));
            __LaunchedEffect(targetState, (!__composer.ChangedAsStruct((targetState, previousValue, targetValue)) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
            {
                previousValue.Value = targetValue.Value;
                targetValue.Value = targetState;
            })), __composer: __composer, __changed: (__dirty & 0b_00_11));
            var resolvedTransition = (!__composer.Changed(targetState!) ? __composer.RememberedValueAsStruct<global::UnityCompose.ContentTransform>() : __composer.UpdateRememberedValueAsStruct<global::UnityCompose.ContentTransform>(Equals(previousValue.Value, targetState) ? IEnterTransition.Empty().TogetherWith(Hide()) : transitionSpec(new AnimatedContentTransitionScopeImpl<T>(previousValue.Value, targetState))));
            var transitionDuration = resolvedTransition.TotalDuration;
            var progress = __AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: transitionDuration), __composer: __composer, __changed: 0).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var resolvedTimeElapsed = resolvedProgress * transitionDuration;
            var(containerModifier, contentModifier) = sizeAnimationSpec.HasValue ? __composer.WithReplaceGroup(1138676624, () => __AnimateSizeModifiers(sizeAnimationSpec.Value, key: targetState, __composer: __composer, __changed: ((__dirty & 0b_00_11) << 2))) : __composer.WithReplaceGroup(1488763163, () => (Modifier, Modifier));
            __ReusableComposeView<AnimatedContent>(modifier: modifier.OrEmpty().Then(containerModifier), content: (!__composer.ChangedAsStruct((targetState, content, isSwitched, previousValue, resolvedTransition, resolvedProgress, resolvedTimeElapsed, contentModifier)) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                var parent = __composer.GetParentVisualElement().NotNull();
                var nextModifier = resolvedTransition.Enter.Get(resolvedTimeElapsed, parent).Then(contentModifier);
                var previousModifier = resolvedTransition.Exit.Get(resolvedTimeElapsed, parent).Float();
                var isAnimationRunning = resolvedProgress is> 0 and < 1;
                var next = (Value: targetState, Modifier: nextModifier, ContentState: isAnimationRunning ? TransitionState.Idle : TransitionState.Entering);
                var previous = (Value: previousValue.Value, Modifier: previousModifier, ContentState: TransitionState.Exiting);
                var pair = isSwitched.Value ? (First: next, Second: previous) : (First: previous, Second: next);
                __composer.StartReplaceGroup(1085212836);
                if (isSwitched.Value || isAnimationRunning)
                {
                    var state = TransitionResolvedState.Create(state: pair.First.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                    __CompositionLocalProvider(LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: (!__composer.ChangedAsStruct((content, pair)) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => content(pair.First.Value, pair.First.Modifier))), __composer: __composer, __changed: 0);
                }

                __composer.EndReplaceGroup(1085212836);
                __composer.StartReplaceGroup(1561194559);
                if (!isSwitched.Value || isAnimationRunning)
                {
                    var state = TransitionResolvedState.Create(state: pair.Second.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                    __CompositionLocalProvider(LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: (!__composer.ChangedAsStruct((content, pair)) ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => content(pair.Second.Value, pair.Second.Modifier))), __composer: __composer, __changed: 0);
                }

                __composer.EndReplaceGroup(1561194559);
            })), __composer: __composer, __changed: 0b_01_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1366895982, __isRestarted)?.UpdateScope(() => __AnimatedContent(__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier, __composer, __dirtyRestart));
    }
}