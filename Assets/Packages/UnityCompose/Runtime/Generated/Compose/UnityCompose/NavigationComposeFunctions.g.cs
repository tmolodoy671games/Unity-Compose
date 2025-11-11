using System;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using static SharpExtensions.CustomSwitch;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    [Compiled]
    private static void __Navigation(IComposeCoordinator coordinator, Func<ContentTransform>? transition = null, IImmutableStableList<ComposeScreen>? initialScreens = null, Action<float>? onTransitionProgressChanged = null, Action<INavigationScope>? content = null, IModifier? modifier = null)
    {
        if (CurrentComposer.BeginComposeGroup((coordinator, transition, initialScreens, onTransitionProgressChanged, content, modifier)))
            return;
        try
        {
            var backStack = Remember(() => MutableStateListOf(initialScreens.OrEmpty().ToImmutableStableList()));
            var coordinatorEntry = LocalCoordinator.Current;
            var parentCoordinator = coordinatorEntry.Coordinator;
            var navigator = Remember<IComposeNavigator>((parentCoordinator, backStack), () => new ComposeNavigatorImpl(backStack, parentCoordinator));
            DisposableEffect(key: coordinator, body: Remember<global::System.Func<global::UnityCompose.IDisposableEffectScope, global::System.IDisposable>>((coordinator, navigator), it =>
            {
                coordinator.CommandBuffer.SetNavigator(navigator);
                return it.OnDispose(() => coordinator.CommandBuffer.RemoveNavigator());
            }));
            var isSwitched = Remember(() => MutableStateOf(false));
            var currentBackStack = backStack.GetOrDefault(backStack.Count - 1, IImmutableStableList.Empty<ComposeScreen>());
            var previousBackStack = Remember(() => IMutableStableProperty.Create(initialScreens.OrEmpty().ToImmutableStableList()));
            LaunchedEffect(currentBackStack, Remember<global::System.Action>((isSwitched, currentBackStack, previousBackStack), () =>
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
            ReusableComposeView<Navigation>(modifier: modifier, content: RememberComposable<global::System.Action>((coordinator, onTransitionProgressChanged, coordinatorEntry, currentBackStack, previousBackStack, appearingScreens, disappearingScreens, allScreens, resolvedTransition, resolvedProgress, isTransitionFinished, resolvedDuration), () =>
            {
                LaunchedEffect(resolvedProgress, Remember<global::System.Action>((onTransitionProgressChanged, resolvedProgress), () => onTransitionProgressChanged?.Invoke(resolvedProgress)));
                if (IsInPreview)
                    return;
                foreach (var screen in allScreens)
                {
                    var screenState = Remember((screen, currentBackStack, previousBackStack.Value), () => Switch().Case(appearingScreens.Contains(screen), ContentState.Entering).Case(disappearingScreens.Contains(screen), ContentState.Exiting).Default(ContentState.Idle).Get());
                    if (screenState == ContentState.Exiting && isTransitionFinished)
                        continue;
                    if (screenState == ContentState.Entering && isTransitionFinished)
                        screenState = ContentState.Idle;
                    Key(key: screen, content: RememberComposable<global::System.Action>((coordinator, coordinatorEntry, currentBackStack, resolvedTransition, resolvedProgress, resolvedDuration, screen, screenState), () =>
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
                        CompositionLocalProvider(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)), LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: isActive)), LocalModifier.Provides(after: LocalModifier.Current.After.OrEmpty().Then(contentStyle)), LocalContentState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: Remember(scope, scope.Content));
                    }));
                }
            }));
            if (isTransitionFinished)
                previousBackStack.Value = currentBackStack;
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Navigation(coordinator, transition, initialScreens, onTransitionProgressChanged, content, modifier));
        }
    }
}

internal partial class NavigationScopeImpl
{
    [Composable]
    [Compiled]
    private void __Content()
    {
        if (CurrentComposer.BeginComposeGroup(null))
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