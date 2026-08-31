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
        var __isCreated = __composer.StartRestartGroup(370710639);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_00_00_11) == 0)
            __dirty |= __composer.Changed(targetState) ? 0b_00_00_00_00_10 : 0b_00_00_00_00_01;
        if ((__changed & 0b_00_00_00_11_00) == 0)
            __dirty |= __composer.Changed(transitionSpec) ? 0b_00_00_00_10_00 : 0b_00_00_00_01_00;
        if ((__changed & 0b_00_00_11_00_00) == 0)
            __dirty |= __composer.Changed(content) ? 0b_00_00_10_00_00 : 0b_00_00_01_00_00;
        if ((__changed & 0b_00_11_00_00_00) == 0)
            __dirty |= __composer.Changed(sizeAnimationSpec) ? 0b_00_10_00_00_00 : 0b_00_01_00_00_00;
        if ((__changed & 0b_11_00_00_00_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00_00_00 : 0b_01_00_00_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01_01_01)
        {
            var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
            __SideEffect(targetState, (!__composer.Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!) ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() => isSwitched.Value = !isSwitched.Value)), __composer: __composer, __changed: (__dirty & 0b_00_11));
            var previousValue = (!__composer.Changed() ? __composer.RememberedValue<global::StableCollections.IMutableStableProperty<T>>() : __composer.UpdateRememberedValue<global::StableCollections.IMutableStableProperty<T>>(IMutableStableProperty.Create(targetState)));
            var targetValue = (!__composer.Changed() ? __composer.RememberedValue<global::StableCollections.IMutableStableProperty<T>>() : __composer.UpdateRememberedValue<global::StableCollections.IMutableStableProperty<T>>(IMutableStableProperty.Create(targetState)));
            __SideEffect(targetState, (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_11) == 0b_00_00_00_00_10).Changed<global::StableCollections.IMutableStableProperty<T>>(previousValue!).Changed<global::StableCollections.IMutableStableProperty<T>>(targetValue!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
            {
                previousValue.Value = targetValue.Value;
                targetValue.Value = targetState;
            })), __composer: __composer, __changed: (__dirty & 0b_00_11));
            var resolvedTransition = (!__composer.BuildChanged().Changed().ChangedAsFlag((__dirty & 0b_00_00_00_00_11) == 0b_00_00_00_00_10).Get() ? __composer.RememberedValue<global::UnityCompose.ContentTransform>() : __composer.UpdateRememberedValue<global::UnityCompose.ContentTransform>(Equals(previousValue.Value, targetState) ? IEnterTransition.Empty().TogetherWith(Hide()) : transitionSpec(new AnimatedContentTransitionScopeImpl<T>(previousValue.Value, targetState))));
            var transitionDuration = resolvedTransition.TotalDuration;
            var progress = __AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: transitionDuration), __composer: __composer, __changed: 0b_00_00).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var resolvedTimeElapsed = resolvedProgress * transitionDuration;
            var(containerModifier, contentModifier) = sizeAnimationSpec.HasValue ? __composer.WithReplaceGroup(1336359455, () => __AnimateSizeModifiers(sizeAnimationSpec.Value, key: targetState, __composer: __composer, __changed: ((__dirty & 0b_00_11) << 2))) : __composer.WithReplaceGroup(1015661743, () => (Modifier, Modifier));
            __ReusableComposeView<AnimatedContent>(modifier: modifier.OrEmpty().Then(containerModifier), content: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_00_00_11) == 0b_00_00_00_00_10).ChangedAsFlag((__dirty & 0b_00_00_11_00_00) == 0b_00_00_10_00_00).Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!).Changed<global::StableCollections.IMutableStableProperty<T>>(previousValue!).Changed<global::UnityCompose.ContentTransform>(resolvedTransition!).Changed<float>(resolvedProgress!).Changed<float>(resolvedTimeElapsed!).Changed<global::UnityCompose.IModifier>(contentModifier!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                var parent = __composer.GetParentVisualElement().NotNull();
                var nextModifier = resolvedTransition.Enter.Get(resolvedTimeElapsed, parent).Then(contentModifier);
                var previousModifier = resolvedTransition.Exit.Get(resolvedTimeElapsed, parent).Float();
                var isAnimationRunning = resolvedProgress is> 0 and < 1;
                var next = (Value: targetState, Modifier: nextModifier, ContentState: isAnimationRunning ? TransitionPhase.Idle : TransitionPhase.Entering);
                var previous = (Value: previousValue.Value, Modifier: previousModifier, ContentState: TransitionPhase.Exiting);
                var pair = isSwitched.Value ? (First: next, Second: previous) : (First: previous, Second: next);
                __composer.StartReplaceGroup(1548521109);
                if (isSwitched.Value || isAnimationRunning)
                {
                    var state = TransitionState.Create(phase: pair.First.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                    var resolvedState = TransitionState.Create(state, LocalResolvedTransitionState.Current);
                    __composer.StartReplaceGroup(121955726);
                    __CompositionLocalProvider(LocalTransitionState.Provides(state), LocalResolvedTransitionState.Provides(resolvedState), content: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_11_00_00) == 0b_00_00_10_00_00).Changed<((T Value, global::UnityCompose.IModifier Modifier, global::UnityCompose.TransitionPhase ContentState) First, (T Value, global::UnityCompose.IModifier Modifier, global::UnityCompose.TransitionPhase ContentState) Second)>(pair!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => content(pair.First.Value, pair.First.Modifier))), __composer: __composer, __changed: 0b_00_00_00);
                    __composer.EndReplaceGroup(121955726);
                }

                __composer.EndReplaceGroup(1548521109);
                __composer.StartReplaceGroup(642540411);
                if (!isSwitched.Value || isAnimationRunning)
                {
                    var state = TransitionState.Create(phase: pair.Second.ContentState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                    var resolvedState = TransitionState.Create(state, LocalResolvedTransitionState.Current);
                    __composer.StartReplaceGroup(1721929633);
                    __CompositionLocalProvider(LocalTransitionState.Provides(state), LocalResolvedTransitionState.Provides(resolvedState), content: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_11_00_00) == 0b_00_00_10_00_00).Changed<((T Value, global::UnityCompose.IModifier Modifier, global::UnityCompose.TransitionPhase ContentState) First, (T Value, global::UnityCompose.IModifier Modifier, global::UnityCompose.TransitionPhase ContentState) Second)>(pair!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => content(pair.Second.Value, pair.Second.Modifier))), __composer: __composer, __changed: 0b_00_00_00);
                    __composer.EndReplaceGroup(1721929633);
                }

                __composer.EndReplaceGroup(642540411);
            })), __composer: __composer, __changed: 0b_01_00);
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01_01_01;
        __composer.EndRestartGroup(370710639, __isRestarted)?.UpdateScope(() => __AnimatedContent(__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}