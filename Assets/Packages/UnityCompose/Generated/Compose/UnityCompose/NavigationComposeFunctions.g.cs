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
    [Composable]
    private static void __Navigation(IComposeCoordinator coordinator, Optional<ContentTransform> transition = default, IModifier? modifier = null)
    {
        var(__coordinator, __transition, __modifier) = (coordinator, transition, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-112916579);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__coordinator, __transition, __modifier)))
        {
            var initialScreens = Remember(coordinator.InitialScreens);
            var backStack = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(MutableStateListOf(initialScreens));
            var coordinatorEntry = LocalCoordinator.Current;
            var parentCoordinator = coordinatorEntry.Coordinator;
            IComposeNavigator navigator = !__composer.ChangedAsStruct((parentCoordinator, backStack)) ? __composer.RememberedValue<UnityCompose.ComposeNavigatorImpl>() : __composer.UpdateRememberedValue<UnityCompose.ComposeNavigatorImpl>(new ComposeNavigatorImpl(backStack, parentCoordinator));
            DisposableEffect(key: coordinator, effect: !__composer.ChangedAsStruct((coordinator, navigator)) ? __composer.RememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : __composer.UpdateRememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
            {
                coordinator.CommandBuffer.SetNavigator(navigator);
                return it.OnDispose(() => coordinator.CommandBuffer.RemoveNavigator());
            }));
            var isSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
            var currentBackStack = backStack.GetOrDefault(backStack.Count - 1, ImmutableStableListOf<ComposeScreen>());
            var previousBackStack = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(MutableStablePropertyOf(initialScreens.OrEmpty().ToImmutableStableList()));
            LaunchedEffect(currentBackStack, !__composer.ChangedAsStruct((isSwitched, currentBackStack, previousBackStack)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
            {
                if (!Equals(currentBackStack, previousBackStack.Value))
                    isSwitched.Value = !isSwitched.Value;
            }));
            var appearingScreens = !__composer.ChangedAsStruct((currentBackStack, previousBackStack.Value)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(currentBackStack.WhereNot(previousBackStack.Value.Contains).ToImmutableStableList());
            var disappearingScreens = !__composer.ChangedAsStruct((currentBackStack, previousBackStack.Value)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(previousBackStack.Value.WhereNot(currentBackStack.Contains).ToImmutableStableList());
            var allScreens = !__composer.ChangedAsStruct((currentBackStack, previousBackStack.Value)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(previousBackStack.Value.Union(currentBackStack).Distinct().OrderBy(static it => it.Priority).ToImmutableStableList());
            var resolvedTransition = !__composer.ChangedAsStruct((transition, appearingScreens, disappearingScreens)) ? __composer.RememberedValueAsStruct<UnityCompose.ContentTransform>() : __composer.UpdateRememberedValueAsStruct<UnityCompose.ContentTransform>(ResolveTransition(transition, appearingScreens, disappearingScreens));
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: resolvedTransition.TotalDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var isTransitionFinished = resolvedProgress.AlmostEquals(1f);
            var resolvedDuration = resolvedProgress * resolvedTransition.TotalDuration;
            var screensToRender = !__composer.ChangedAsStruct((allScreens, appearingScreens, disappearingScreens, isTransitionFinished)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<(UnityCompose.ComposeScreen Screen, UnityCompose.TransitionState ScreenState)>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<(UnityCompose.ComposeScreen Screen, UnityCompose.TransitionState ScreenState)>>(allScreens.Select(screen =>
            {
                var screenState = Switch().Case(appearingScreens.Contains(screen), TransitionState.Entering).Case(disappearingScreens.Contains(screen), TransitionState.Exiting).Default(TransitionState.Idle).Get();
                if (screenState == TransitionState.Entering && isTransitionFinished)
                    screenState = TransitionState.Idle;
                return (Screen: screen, ScreenState: screenState);
            }).Where(it => it.ScreenState != TransitionState.Exiting || !isTransitionFinished).ToImmutableStableList());
            ReusableComposeView<Navigation>(modifier: modifier, content: !__composer.ChangedAsStruct((coordinator, coordinatorEntry, currentBackStack, resolvedTransition, resolvedProgress, resolvedDuration, screensToRender)) ? __composer.RememberedValue<UnityCompose.ComposableContent?>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent?>(() =>
            {
                foreach (var(screen, screenState)in screensToRender)
                {
                    Key(key: screen, content: !__composer.ChangedAsStruct((coordinator, coordinatorEntry, currentBackStack, resolvedTransition, resolvedProgress, resolvedDuration, screen, screenState)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var parent = CurrentComposer.GetParentVisualElement().NotNull();
                        var isCurrentScreen = screen.Equals(currentBackStack!.GetOrDefault(currentBackStack.Count - 1, null));
                        var contentModifier = screenState switch
                        {
                            TransitionState.Idle => Modifier.Float(!isCurrentScreen),
                            TransitionState.Entering => resolvedTransition.Enter.Get(resolvedDuration, parent).Float(!isCurrentScreen),
                            TransitionState.Exiting => resolvedTransition.Exit.Get(resolvedDuration, parent).Float(),
                            _ => throw new ArgumentOutOfRangeException()};
                        var state = TransitionResolvedState.Create(state: screenState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        var isActive = LocalIsActive.Current;
                        CompositionLocalProvider(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)), LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: isActive)), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: !__composer.ChangedAsStruct((screen, contentModifier)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() => screen.Content(contentModifier)));
                    }));
                }
            }));
            if (isTransitionFinished)
                previousBackStack.Value = currentBackStack;
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-112916579, __isRestarted)?.UpdateScope(() => __Navigation(__coordinator, __transition, __modifier));
    }
}

internal partial class NavigationScopeImpl
{
    [Composable]
    private void __Content()
    {
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(-516650310);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecute())
        {
            _content();
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(-516650310, __isRestarted)?.UpdateScope(() => __Content());
    }
}