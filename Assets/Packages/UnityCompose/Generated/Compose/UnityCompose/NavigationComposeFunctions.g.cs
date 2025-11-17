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
    public static void __Navigation(IComposeCoordinator coordinator, Func<ContentTransform>? transition = null, IImmutableStableList<ComposeScreen>? initialScreens = null, Action<float>? onTransitionProgressChanged = null, Action<INavigationScope>? content = null, IModifier? modifier = null)
    {
        var(__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier) = (coordinator, transition, initialScreens, onTransitionProgressChanged, content, modifier);
        if (CurrentComposer.BeginComposeGroup((__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier)))
            return;
        try
        {
            var backStack = Remember(CurrentComposer.WithState(initialScreens).Remember<Func>(__ => () => MutableStateListOf(initialScreens.OrEmpty().ToImmutableStableList())));
            var coordinatorEntry = LocalCoordinator.Current;
            var parentCoordinator = coordinatorEntry.Coordinator;
            IComposeNavigator navigator = Remember((parentCoordinator, backStack), CurrentComposer.WithState((backStack, parentCoordinator)).Remember<Func>(__ => () => new ComposeNavigatorImpl(backStack, parentCoordinator)));
            DisposableEffect(key: coordinator, effect: CurrentComposer.WithState((coordinator, navigator)).Remember<Func>(__ => it =>
            {
                coordinator.CommandBuffer.SetNavigator(navigator);
                return it.OnDispose(CurrentComposer.WithState(__.coordinator).Remember<Action>(__ => () => coordinator.CommandBuffer.RemoveNavigator()));
            }));
            var isSwitched = Remember(static () => MutableStateOf(false));
            var currentBackStack = backStack.GetOrDefault(backStack.Count - 1, IImmutableStableList.Empty<ComposeScreen>());
            var previousBackStack = Remember(CurrentComposer.WithState(initialScreens).Remember<Func>(__ => () => IMutableStableProperty.Create(initialScreens.OrEmpty().ToImmutableStableList())));
            LaunchedEffect(currentBackStack, CurrentComposer.WithState((isSwitched, currentBackStack, previousBackStack)).Remember<Action>(__ => () =>
            {
                if (!Equals(currentBackStack, previousBackStack.Value))
                    isSwitched.Value = !isSwitched.Value;
            }));
            var appearingScreens = Remember((currentBackStack, previousBackStack.Value), CurrentComposer.WithState((currentBackStack, previousBackStack)).Remember<Func>(__ => () => currentBackStack.WhereNot(previousBackStack.Value.Contains).ToImmutableStableList()));
            var disappearingScreens = Remember((currentBackStack, previousBackStack.Value), CurrentComposer.WithState((currentBackStack, previousBackStack)).Remember<Func>(__ => () => previousBackStack.Value.WhereNot(currentBackStack.Contains).ToImmutableStableList()));
            var allScreens = Remember((currentBackStack, previousBackStack.Value), CurrentComposer.WithState((currentBackStack, previousBackStack)).Remember<Func>(__ => () => previousBackStack.Value.Union(currentBackStack).Distinct().OrderBy(CurrentComposer.WithState((__.transition, __.appearingScreens, __.disappearingScreens)).Remember<Func>(__ => () => ResolveTransition(transition, appearingScreens, disappearingScreens))).ToImmutableStableList()));
            var resolvedTransition = Remember((appearingScreens, disappearingScreens, transition), CurrentComposer.WithState((coordinator, onTransitionProgressChanged, content, coordinatorEntry, currentBackStack, previousBackStack, appearingScreens, disappearingScreens, allScreens, resolvedTransition, resolvedProgress, isTransitionFinished, resolvedDuration)).Remember<Action>(__ => () =>
            {
                LaunchedEffect(resolvedProgress, CurrentComposer.WithState((__.onTransitionProgressChanged, __.resolvedProgress)).Remember<Action>(__ => () => onTransitionProgressChanged?.Invoke(resolvedProgress)));
                if (IsInPreview)
                    return;
                foreach (var screen in allScreens)
                {
                    var screenState = Remember((screen, currentBackStack, previousBackStack.Value), CurrentComposer.WithState((__.appearingScreens, __.disappearingScreens, __.screen)).Remember<Func>(__ => () => Switch().Case(appearingScreens.Contains(screen), ContentState.Entering).Case(disappearingScreens.Contains(screen), ContentState.Exiting).Default(ContentState.Idle).Get()));
                    if (screenState == ContentState.Exiting && isTransitionFinished)
                        continue;
                    if (screenState == ContentState.Entering && isTransitionFinished)
                        screenState = ContentState.Idle;
                    Key(key: screen, content: CurrentComposer.WithState((__.coordinator, __.content, __.coordinatorEntry, __.currentBackStack, __.resolvedTransition, __.resolvedProgress, __.resolvedDuration, __.screen, __.screenState)).Remember<Action>(__ => () =>
                    {
                        var parent = LocalVisualElement.Current;
                        var isCurrentScreen = screen.Equals(currentBackStack[^1]);
                        var contentStyle = screenState switch
                        {
                            ContentState.Idle => Modifier.Float(!isCurrentScreen),
                            ContentState.Entering => resolvedTransition.Enter.Get(resolvedDuration, parent).Float(!isCurrentScreen),
                            ContentState.Exiting => resolvedTransition.Exit.Get(resolvedDuration, parent).Float(),
                            _ => throw new ArgumentOutOfRangeException()};
                        var state = TransitionState.Create(state: screenState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        var isActive = LocalIsActive.Current;
                        var scope = Remember(screen, CurrentComposer.WithState(__.screen).Remember<Func>(__ => () => new NavigationScopeImpl(screen.Content)));
                        CompositionLocalProvider(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)), LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: isActive)), LocalModifier.Provides(after: LocalModifier.Current.After.OrEmpty().Then(contentStyle)), LocalContentState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: CurrentComposer.WithState((__.content, __.scope)).Remember<Action>(__ => () =>
                        {
                            if (content != null)
                                content(scope);
                            else
                                scope.Content();
                        }));
                    }));
                }
            }));
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: resolvedTransition.TotalDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var isTransitionFinished = resolvedProgress.AlmostEquals(1f);
            var resolvedDuration = resolvedProgress * resolvedTransition.TotalDuration;
            ReusableComposeView<Navigation>(modifier: modifier, content: () =>
            {
                LaunchedEffect(resolvedProgress, () => onTransitionProgressChanged?.Invoke(resolvedProgress));
                if (IsInPreview)
                    return;
                foreach (var screen in allScreens)
                {
                    var screenState = Remember((screen, currentBackStack, previousBackStack.Value), () => Switch().Case(appearingScreens.Contains(screen), ContentState.Entering).Case(disappearingScreens.Contains(screen), ContentState.Exiting).Default(ContentState.Idle).Get());
                    if (screenState == ContentState.Exiting && isTransitionFinished)
                        continue;
                    if (screenState == ContentState.Entering && isTransitionFinished)
                        screenState = ContentState.Idle;
                    Key(key: screen, content: () =>
                    {
                        var parent = LocalVisualElement.Current;
                        var isCurrentScreen = screen.Equals(currentBackStack[^1]);
                        var contentStyle = screenState switch
                        {
                            ContentState.Idle => Modifier.Float(!isCurrentScreen),
                            ContentState.Entering => resolvedTransition.Enter.Get(resolvedDuration, parent).Float(!isCurrentScreen),
                            ContentState.Exiting => resolvedTransition.Exit.Get(resolvedDuration, parent).Float(),
                            _ => throw new ArgumentOutOfRangeException()};
                        var state = TransitionState.Create(state: screenState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        var isActive = LocalIsActive.Current;
                        var scope = Remember(screen, () => new NavigationScopeImpl(screen.Content));
                        CompositionLocalProvider(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)), LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: isActive)), LocalModifier.Provides(after: LocalModifier.Current.After.OrEmpty().Then(contentStyle)), LocalContentState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: () =>
                        {
                            if (content != null)
                                content(scope);
                            else
                                scope.Content();
                        });
                    });
                }
            });
            if (isTransitionFinished)
                previousBackStack.Value = currentBackStack;
        }
        finally
        {
            CurrentComposer.EndComposeGroup(CurrentComposer.WithState((__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier)).Remember<Action>(static __ => () => __Navigation(__.__coordinator, __.__transition, __.__initialScreens, __.__onTransitionProgressChanged, __.__content, __.__modifier)));
        }
    }
}

internal partial class NavigationScopeImpl : INavigationScope, IEquatable<NavigationScopeImpl>
{
    [Composable]
    public void __Content()
    {
        if (CurrentComposer.BeginComposeGroup(string.Empty))
            return;
        try
        {
            _content();
        }
        finally
        {
            CurrentComposer.EndComposeGroup(static () => __Content());
        }
    }
}