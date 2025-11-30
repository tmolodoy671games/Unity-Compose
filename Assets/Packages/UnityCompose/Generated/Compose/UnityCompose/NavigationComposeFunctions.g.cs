using System;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using static SharpExtensions.CustomSwitch;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __Navigation(IComposeCoordinator coordinator, Func<ContentTransform>? transition = null, IImmutableStableList<ComposeScreen>? initialScreens = null, Action<float>? onTransitionProgressChanged = null, Action<INavigationScope>? content = null, IModifier? modifier = null)
    {
        var(__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier) = (coordinator, transition, initialScreens, onTransitionProgressChanged, content, modifier);
        if (CurrentComposer.BeginComposeGroup(1768390015, (__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier)))
            return;
        try
        {
            var backStack = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(-122087210, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(() => MutableStateListOf(initialScreens.OrEmpty().ToImmutableStableList()));
            var coordinatorEntry = LocalCoordinator.Current;
            var parentCoordinator = coordinatorEntry.Coordinator;
            IComposeNavigator navigator = CurrentComposer.HasRememberedValue<(UnityCompose.IComposeCoordinator? parentCoordinator, UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>> backStack), UnityCompose.ComposeNavigatorImpl>(-412119540, (parentCoordinator, backStack)) ? CurrentComposer.RememberedValue<(UnityCompose.IComposeCoordinator? parentCoordinator, UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>> backStack), UnityCompose.ComposeNavigatorImpl>() : CurrentComposer.WriteValue<(UnityCompose.IComposeCoordinator? parentCoordinator, UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>> backStack), UnityCompose.ComposeNavigatorImpl>(() => new ComposeNavigatorImpl(backStack, parentCoordinator));
            DisposableEffect(key: coordinator, effect: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.IComposeCoordinator, UnityCompose.IComposeNavigator>, System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(-1578792728, (coordinator, navigator)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.IComposeCoordinator, UnityCompose.IComposeNavigator>, System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.IComposeCoordinator, UnityCompose.IComposeNavigator>, System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
            {
                coordinator.CommandBuffer.SetNavigator(navigator);
                return it.OnDispose(() => coordinator.CommandBuffer.RemoveNavigator());
            }));
            var isSwitched = CurrentComposer.HasRememberedValue<bool, UnityCompose.IMutableState<bool>>(-525124656, true) ? CurrentComposer.RememberedValue<bool, UnityCompose.IMutableState<bool>>() : CurrentComposer.WriteValue<bool, UnityCompose.IMutableState<bool>>(() => MutableStateOf(false));
            var currentBackStack = backStack.GetOrDefault(backStack.Count - 1, IImmutableStableList.Empty<ComposeScreen>());
            var previousBackStack = CurrentComposer.HasRememberedValue<bool, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(229066550, true) ? CurrentComposer.RememberedValue<bool, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>() : CurrentComposer.WriteValue<bool, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(() => IMutableStableProperty.Create(initialScreens.OrEmpty().ToImmutableStableList()));
            LaunchedEffect(currentBackStack, CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.IMutableState<bool>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>?>, System.Action>(-1405182596, (isSwitched, currentBackStack, previousBackStack)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.IMutableState<bool>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>?>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.IMutableState<bool>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>?>, System.Action>(() =>
            {
                if (!Equals(currentBackStack, previousBackStack.Value))
                    isSwitched.Value = !isSwitched.Value;
            }));
            var appearingScreens = CurrentComposer.HasRememberedValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(618259719, (currentBackStack, previousBackStack.Value)) ? CurrentComposer.RememberedValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : CurrentComposer.WriteValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(() => currentBackStack.WhereNot(previousBackStack.Value.Contains).ToImmutableStableList());
            var disappearingScreens = CurrentComposer.HasRememberedValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(-1099612885, (currentBackStack, previousBackStack.Value)) ? CurrentComposer.RememberedValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : CurrentComposer.WriteValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(() => previousBackStack.Value.WhereNot(currentBackStack.Contains).ToImmutableStableList());
            var allScreens = CurrentComposer.HasRememberedValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(1710090918, (currentBackStack, previousBackStack.Value)) ? CurrentComposer.RememberedValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : CurrentComposer.WriteValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(() => previousBackStack.Value.Union(currentBackStack).Distinct().OrderBy(static it => it.Priority).ToImmutableStableList());
            var resolvedTransition = CurrentComposer.HasRememberedValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> appearingScreens, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> disappearingScreens, System.Func<UnityCompose.ContentTransform>? transition), UnityCompose.ContentTransform>(1474599597, (appearingScreens, disappearingScreens, transition)) ? CurrentComposer.RememberedValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> appearingScreens, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> disappearingScreens, System.Func<UnityCompose.ContentTransform>? transition), UnityCompose.ContentTransform>() : CurrentComposer.WriteValue<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> appearingScreens, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> disappearingScreens, System.Func<UnityCompose.ContentTransform>? transition), UnityCompose.ContentTransform>(() => ResolveTransition(transition, appearingScreens, disappearingScreens));
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: resolvedTransition.TotalDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var isTransitionFinished = resolvedProgress.AlmostEquals(1f);
            var resolvedDuration = resolvedProgress * resolvedTransition.TotalDuration;
            ReusableComposeView<Navigation>(modifier: modifier, content: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.IComposeCoordinator, System.Action<float>?, System.Action<UnityCompose.INavigationScope>?, UnityCompose.ComposeFunctions.CoordinatorEntry?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, ValueTuple<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, UnityCompose.ContentTransform, float, bool, float>>, UnityCompose.ComposableContent?>(373609324, (coordinator, onTransitionProgressChanged, content, coordinatorEntry, currentBackStack, previousBackStack, appearingScreens, disappearingScreens, allScreens, resolvedTransition, resolvedProgress, isTransitionFinished, resolvedDuration)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.IComposeCoordinator, System.Action<float>?, System.Action<UnityCompose.INavigationScope>?, UnityCompose.ComposeFunctions.CoordinatorEntry?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, ValueTuple<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, UnityCompose.ContentTransform, float, bool, float>>, UnityCompose.ComposableContent?>() : CurrentComposer.WriteComposableLambda<ValueTuple<UnityCompose.IComposeCoordinator, System.Action<float>?, System.Action<UnityCompose.INavigationScope>?, UnityCompose.ComposeFunctions.CoordinatorEntry?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, ValueTuple<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, UnityCompose.ContentTransform, float, bool, float>>, UnityCompose.ComposableContent?>(() =>
            {
                LaunchedEffect(resolvedProgress, CurrentComposer.HasRememberedValue<ValueTuple<System.Action<float>?, float>, System.Action>(-1820451197, (onTransitionProgressChanged, resolvedProgress)) ? CurrentComposer.RememberedValue<ValueTuple<System.Action<float>?, float>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<System.Action<float>?, float>, System.Action>(() => onTransitionProgressChanged?.Invoke(resolvedProgress)));
                if (IsInPreview)
                    return;
                foreach (var screen in allScreens)
                {
                    var screenState = CurrentComposer.HasRememberedValue<(UnityCompose.ComposeScreen screen, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), UnityCompose.TransitionState>(-1871528917, (screen, currentBackStack, previousBackStack.Value)) ? CurrentComposer.RememberedValue<(UnityCompose.ComposeScreen screen, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), UnityCompose.TransitionState>() : CurrentComposer.WriteValue<(UnityCompose.ComposeScreen screen, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value), UnityCompose.TransitionState>(() => Switch().Case(appearingScreens.Contains(screen), TransitionState.Entering).Case(disappearingScreens.Contains(screen), TransitionState.Exiting).Default(TransitionState.Idle).Get());
                    if (screenState == TransitionState.Exiting && isTransitionFinished)
                        continue;
                    if (screenState == TransitionState.Entering && isTransitionFinished)
                        screenState = TransitionState.Idle;
                    Key(key: screen, content: CurrentComposer.HasRememberedValue<ValueTuple<UnityCompose.IComposeCoordinator, System.Action<UnityCompose.INavigationScope>?, UnityCompose.ComposeFunctions.CoordinatorEntry?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, UnityCompose.ContentTransform, float, float, ValueTuple<UnityCompose.ComposeScreen?, UnityCompose.TransitionState>>, System.Action>(1574762525, (coordinator, content, coordinatorEntry, currentBackStack, resolvedTransition, resolvedProgress, resolvedDuration, screen, screenState)) ? CurrentComposer.RememberedValue<ValueTuple<UnityCompose.IComposeCoordinator, System.Action<UnityCompose.INavigationScope>?, UnityCompose.ComposeFunctions.CoordinatorEntry?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, UnityCompose.ContentTransform, float, float, ValueTuple<UnityCompose.ComposeScreen?, UnityCompose.TransitionState>>, System.Action>() : CurrentComposer.WriteLambda<ValueTuple<UnityCompose.IComposeCoordinator, System.Action<UnityCompose.INavigationScope>?, UnityCompose.ComposeFunctions.CoordinatorEntry?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, UnityCompose.ContentTransform, float, float, ValueTuple<UnityCompose.ComposeScreen?, UnityCompose.TransitionState>>, System.Action>(() =>
                    {
                        var parent = LocalVisualElement.Current;
                        var isCurrentScreen = screen.Equals(currentBackStack[^1]);
                        var contentModifier = screenState switch
                        {
                            TransitionState.Idle => Modifier.Float(!isCurrentScreen),
                            TransitionState.Entering => resolvedTransition.Enter.Get(resolvedDuration, parent).Float(!isCurrentScreen),
                            TransitionState.Exiting => resolvedTransition.Exit.Get(resolvedDuration, parent).Float(),
                            _ => throw new ArgumentOutOfRangeException()};
                        var state = TransitionResolvedState.Create(state: screenState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        var isActive = LocalIsActive.Current;
                        var scope = CurrentComposer.HasRememberedValue<UnityCompose.ComposeScreen, UnityCompose.NavigationScopeImpl>(-219983774, screen) ? CurrentComposer.RememberedValue<UnityCompose.ComposeScreen, UnityCompose.NavigationScopeImpl>() : CurrentComposer.WriteValue<UnityCompose.ComposeScreen, UnityCompose.NavigationScopeImpl>(() => new NavigationScopeImpl(screen.Content));
                        CompositionLocalProvider(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)), LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: isActive)), LocalModifier.Provides(after: LocalModifier.Current.After.OrEmpty().Then(contentModifier)), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: () =>
                        {
                            if (content != null)
                                content(scope);
                            else
                                scope.Content();
                        });
                    }));
                }
            }));
            if (isTransitionFinished)
                previousBackStack.Value = currentBackStack;
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<ValueTuple<IComposeCoordinator, Func<ContentTransform>?, IImmutableStableList<ComposeScreen>?, Action<float>?, Action<INavigationScope>?, IModifier?>, Action>(1768490015, (__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier)) ? CurrentComposer.RememberedValue<ValueTuple<IComposeCoordinator, Func<ContentTransform>?, IImmutableStableList<ComposeScreen>?, Action<float>?, Action<INavigationScope>?, IModifier?>, Action>() : CurrentComposer.WriteComposableLambda<ValueTuple<IComposeCoordinator, Func<ContentTransform>?, IImmutableStableList<ComposeScreen>?, Action<float>?, Action<INavigationScope>?, IModifier?>, Action>(() => __Navigation(__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier)));
        }
    }
}

internal partial class NavigationScopeImpl
{
    [Composable]
    private void __Content()
    {
        if (CurrentComposer.BeginComposeGroup(1960824537, true))
            return;
        try
        {
            _content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.HasRememberedValue<bool, Action>(1960924537, true) ? CurrentComposer.RememberedValue<bool, Action>() : CurrentComposer.WriteComposableLambda<bool, Action>(() => __Content()));
        }
    }
}