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
        if (CurrentComposer.BeginComposeGroup((__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier)))
            return;
        try
        {
            var backStack = Remember(() => MutableStateListOf(initialScreens.OrEmpty().ToImmutableStableList()));
            var coordinatorEntry = LocalCoordinator.Current;
            var parentCoordinator = coordinatorEntry.Coordinator;
            IComposeNavigator navigator = Remember((parentCoordinator, backStack), () => new ComposeNavigatorImpl(backStack, parentCoordinator));
            DisposableEffect(key: coordinator, effect: CurrentComposer.WithState((coordinator, navigator)).Remember<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(__ => it =>
            {
                coordinator.CommandBuffer.SetNavigator(navigator);
                return it.OnDispose(CurrentComposer.WithState(coordinator).Remember<System.Action>(__ => () => coordinator.CommandBuffer.RemoveNavigator()));
            }));
            var isSwitched = Remember(() => MutableStateOf(false));
            var currentBackStack = backStack.GetOrDefault(backStack.Count - 1, IImmutableStableList.Empty<ComposeScreen>());
            var previousBackStack = Remember(() => IMutableStableProperty.Create(initialScreens.OrEmpty().ToImmutableStableList()));
            LaunchedEffect(currentBackStack, CurrentComposer.WithState((isSwitched, currentBackStack, previousBackStack)).Remember<System.Action>(__ => () =>
            {
                if (!Equals(currentBackStack, previousBackStack.Value))
                    isSwitched.Value = !isSwitched.Value;
            }));
            var appearingScreens = Remember((currentBackStack, previousBackStack.Value), () => currentBackStack.WhereNot(previousBackStack.Value.Contains).ToImmutableStableList());
            var disappearingScreens = Remember((currentBackStack, previousBackStack.Value), () => previousBackStack.Value.WhereNot(currentBackStack.Contains).ToImmutableStableList());
            var allScreens = Remember((currentBackStack, previousBackStack.Value), () => previousBackStack.Value.Union(currentBackStack).Distinct().OrderBy(static it => it.Priority).ToImmutableStableList());
            var resolvedTransition = Remember((appearingScreens, disappearingScreens, transition), () => ResolveTransition(transition, appearingScreens, disappearingScreens));
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: resolvedTransition.TotalDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var isTransitionFinished = resolvedProgress.AlmostEquals(1f);
            var resolvedDuration = resolvedProgress * resolvedTransition.TotalDuration;
            ReusableComposeView<Navigation>(modifier: modifier, content: CurrentComposer.WithState((coordinator, onTransitionProgressChanged, content, coordinatorEntry, currentBackStack, previousBackStack, appearingScreens, disappearingScreens, allScreens, resolvedTransition, resolvedProgress, isTransitionFinished, resolvedDuration)).Remember<System.Action?>(__ => () =>
            {
                LaunchedEffect(resolvedProgress, CurrentComposer.WithState((onTransitionProgressChanged, resolvedProgress)).Remember<System.Action>(__ => () => onTransitionProgressChanged?.Invoke(resolvedProgress)));
                if (IsInPreview)
                    return;
                foreach (var screen in allScreens)
                {
                    var screenState = Remember((screen, currentBackStack, previousBackStack.Value), () => Switch().Case(appearingScreens.Contains(screen), TransitionState.Entering).Case(disappearingScreens.Contains(screen), TransitionState.Exiting).Default(TransitionState.Idle).Get());
                    if (screenState == TransitionState.Exiting && isTransitionFinished)
                        continue;
                    if (screenState == TransitionState.Entering && isTransitionFinished)
                        screenState = TransitionState.Idle;
                    Key(key: screen, content: CurrentComposer.WithState((coordinator, content, coordinatorEntry, currentBackStack, resolvedTransition, resolvedProgress, resolvedDuration, screen, screenState)).Remember<System.Action>(__ => () =>
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
                        var scope = Remember(screen, () => new NavigationScopeImpl(screen.Content));
                        CompositionLocalProvider(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)), LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: isActive)), LocalModifier.Provides(after: LocalModifier.Current.After.OrEmpty().Then(contentModifier)), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: CurrentComposer.WithState((content, scope)).Remember<System.Action>(__ => () =>
                        {
                            if (content != null)
                                content(scope);
                            else
                                scope.Content();
                        }));
                    }));
                }
            }));
            if (isTransitionFinished)
                previousBackStack.Value = currentBackStack;
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier)).Remember<Action>(__ => () => __Navigation(__.__coordinator, __.__transition, __.__initialScreens, __.__onTransitionProgressChanged, __.__content, __.__modifier)));
        }
    }
}

internal partial class NavigationScopeImpl
{
    [Composable]
    private void __Content()
    {
        if (CurrentComposer.BeginComposeGroup(string.Empty))
            return;
        try
        {
            _content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Content());
        }
    }
}