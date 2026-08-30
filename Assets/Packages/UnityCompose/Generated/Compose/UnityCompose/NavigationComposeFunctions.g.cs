#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using static SharpExtensions.CustomSwitch;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    public static void __Navigation(IComposeCoordinator coordinator, Optional<ContentTransform> transition = default, IModifier? modifier = null, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var(__coordinator, __transition, __modifier) = (coordinator, transition, modifier);
        var __isCreated = __composer.StartRestartGroup(2104044227);
        var __dirty = __changed;
        if ((__changed & 0b_00_00_11) == 0)
            __dirty |= __composer.Changed(coordinator) ? 0b_00_00_10 : 0b_00_00_01;
        if ((__changed & 0b_00_11_00) == 0)
            __dirty |= __composer.Changed(transition) ? 0b_00_10_00 : 0b_00_01_00;
        if ((__changed & 0b_11_00_00) == 0)
            __dirty |= __composer.Changed(modifier) ? 0b_10_00_00 : 0b_01_00_00;
        var __isRestarted = __composer.IsRestarted();
        if (__isCreated || __isRestarted || __dirty != 0b_01_01_01)
        {
            var initialScreens = (!__composer.Changed() ? __composer.RememberedValue<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>(coordinator.InitialScreens));
            var backStack = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableStateList<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableStateList<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>>(MutableStateListOf(initialScreens)));
            var coordinatorEntry = LocalCoordinator.Current;
            var parentCoordinator = coordinatorEntry.Coordinator;
            IComposeNavigator navigator = (!__composer.Changed<(global::UnityCompose.IComposeCoordinator parentCoordinator, global::UnityCompose.IMutableStateList<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>> backStack)>((parentCoordinator, backStack)!) ? __composer.RememberedValue<global::UnityCompose.ComposeNavigatorImpl>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposeNavigatorImpl>(new ComposeNavigatorImpl(backStack, parentCoordinator)));
            __DisposableEffect(key: coordinator, effect: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_11) == 0b_00_00_10).Changed<global::UnityCompose.IComposeNavigator>(navigator!).Get() ? __composer.RememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>() : __composer.UpdateRememberedValue<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::UnityCompose.IDisposableEffectResult>>(it =>
            {
                coordinator.CommandBuffer.SetNavigator(navigator);
                return it.OnDispose(() => coordinator.CommandBuffer.RemoveNavigator());
            })), __composer: __composer, __changed: (__dirty & 0b_00_11));
            var isSwitched = (!__composer.Changed() ? __composer.RememberedValue<global::UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<global::UnityCompose.IMutableState<bool>>(MutableStateOf(false)));
            var currentBackStack = backStack.GetOrDefault(backStack.Count - 1, ImmutableStableListOf<ComposeScreen>());
            var previousBackStack = (!__composer.Changed() ? __composer.RememberedValue<global::StableCollections.IMutableStableProperty<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>>() : __composer.UpdateRememberedValue<global::StableCollections.IMutableStableProperty<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>>(MutableStablePropertyOf(initialScreens.OrEmpty().ToImmutableStableList())));
            __SideEffect(currentBackStack, (!__composer.BuildChanged().Changed<global::UnityCompose.IMutableState<bool>>(isSwitched!).Changed<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>(currentBackStack!).Changed<global::StableCollections.IMutableStableProperty<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>>(previousBackStack!).Get() ? __composer.RememberedValue<global::System.Action>() : __composer.UpdateRememberedValue<global::System.Action>(() =>
            {
                if (!Equals(currentBackStack, previousBackStack.Value))
                    isSwitched.Value = !isSwitched.Value;
            })), __composer: __composer, __changed: 0b_00_00);
            var appearingScreens = (!__composer.Changed<(global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> currentBackStack, global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> Value)>((currentBackStack, previousBackStack.Value)!) ? __composer.RememberedValue<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>(currentBackStack.WhereNot(previousBackStack.Value.Contains).ToImmutableStableList()));
            var disappearingScreens = (!__composer.Changed<(global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> currentBackStack, global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> Value)>((currentBackStack, previousBackStack.Value)!) ? __composer.RememberedValue<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>(previousBackStack.Value.WhereNot(currentBackStack.Contains).ToImmutableStableList()));
            var allScreens = (!__composer.Changed<(global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> currentBackStack, global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> Value)>((currentBackStack, previousBackStack.Value)!) ? __composer.RememberedValue<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>(previousBackStack.Value.Union(currentBackStack).Distinct().OrderBy(static it => it.Priority).ToImmutableStableList()));
            var resolvedTransition = (!__composer.Changed<(global::SharpExtensions.Optional<global::UnityCompose.ContentTransform> transition, global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> appearingScreens, global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> disappearingScreens)>((transition, appearingScreens, disappearingScreens)!) ? __composer.RememberedValue<global::UnityCompose.ContentTransform>() : __composer.UpdateRememberedValue<global::UnityCompose.ContentTransform>(ResolveTransition(transition, appearingScreens, disappearingScreens)));
            var progress = __AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: resolvedTransition.TotalDuration), __composer: __composer, __changed: 0b_01_00_00).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var isTransitionFinished = resolvedProgress.AlmostEquals(1f);
            var resolvedDuration = resolvedProgress * resolvedTransition.TotalDuration;
            var screensToRender = (!__composer.Changed<(global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> allScreens, global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> appearingScreens, global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen> disappearingScreens, bool isTransitionFinished)>((allScreens, appearingScreens, disappearingScreens, isTransitionFinished)!) ? __composer.RememberedValue<global::StableCollections.IImmutableStableList<(global::UnityCompose.ComposeScreen Screen, global::UnityCompose.TransitionPhase ScreenState)>>() : __composer.UpdateRememberedValue<global::StableCollections.IImmutableStableList<(global::UnityCompose.ComposeScreen Screen, global::UnityCompose.TransitionPhase ScreenState)>>(allScreens.Select(screen =>
            {
                var screenState = Switch().Case(appearingScreens.Contains(screen), TransitionPhase.Entering).Case(disappearingScreens.Contains(screen), TransitionPhase.Exiting).Default(TransitionPhase.Idle).Get();
                if (screenState == TransitionPhase.Entering && isTransitionFinished)
                    screenState = TransitionPhase.Idle;
                return (Screen: screen, ScreenState: screenState);
            }).Where(it => it.ScreenState != TransitionPhase.Exiting || !isTransitionFinished).ToImmutableStableList()));
            __ReusableComposeView<Navigation>(modifier: modifier, content: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_11) == 0b_00_00_10).Changed<global::UnityCompose.ComposeFunctions.CoordinatorEntry>(coordinatorEntry!).Changed<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>(currentBackStack!).Changed<global::UnityCompose.ContentTransform>(resolvedTransition!).Changed<float>(resolvedProgress!).Changed<float>(resolvedDuration!).Changed<global::StableCollections.IImmutableStableList<(global::UnityCompose.ComposeScreen Screen, global::UnityCompose.TransitionPhase ScreenState)>>(screensToRender!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
            {
                foreach (var(screen, screenState)in screensToRender)
                {
                    Key(key: screen, content: (!__composer.BuildChanged().ChangedAsFlag((__dirty & 0b_00_00_11) == 0b_00_00_10).Changed<global::UnityCompose.ComposeFunctions.CoordinatorEntry>(coordinatorEntry!).Changed<global::StableCollections.IImmutableStableList<global::UnityCompose.ComposeScreen>>(currentBackStack!).Changed<global::UnityCompose.ContentTransform>(resolvedTransition!).Changed<float>(resolvedProgress!).Changed<float>(resolvedDuration!).Changed<global::UnityCompose.ComposeScreen>(screen!).Changed<global::UnityCompose.TransitionPhase>(screenState!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() =>
                    {
                        var parent = __composer.GetParentVisualElement().NotNull();
                        var isCurrentScreen = screen.Equals(currentBackStack!.GetOrDefault(currentBackStack.Count - 1, null));
                        var contentModifier = screenState switch
                        {
                            TransitionPhase.Idle => Modifier.Float(!isCurrentScreen),
                            TransitionPhase.Entering => resolvedTransition.Enter.Get(resolvedDuration, parent).Float(!isCurrentScreen),
                            TransitionPhase.Exiting => resolvedTransition.Exit.Get(resolvedDuration, parent).Float(),
                            _ => throw new ArgumentOutOfRangeException()};
                        var state = TransitionState.Create(phase: screenState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        var resolvedState = TransitionState.Create(state, LocalResolvedTransitionState.Current);
                        var isActive = LocalIsActive.Current;
                        __CompositionLocalProvider(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)), LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: isActive)), LocalTransitionState.Provides(state), LocalResolvedTransitionState.Provides(resolvedState), content: (!__composer.BuildChanged().Changed<global::UnityCompose.ComposeScreen>(screen!).Changed<global::UnityCompose.IModifier>(contentModifier!).Get() ? __composer.RememberedValue<global::UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<global::UnityCompose.ComposableContent>(() => screen.__Content(contentModifier, __composer: __composer, __changed: 0b_00))), __composer: __composer, __changed: 0b_00_00_00_00_00);
                    })));
                }
            })), __composer: __composer, __changed: 0b_01_00 | ((__dirty & 0b_11_00_00) >> 4));
            if (isTransitionFinished)
                previousBackStack.Value = currentBackStack;
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __dirty = 0b_01_01_01;
        __composer.EndRestartGroup(2104044227, __isRestarted)?.UpdateScope(() => __Navigation(__coordinator, __transition, __modifier, __composer, __composer.UpdateChangedFlags(__changed)));
    }
}