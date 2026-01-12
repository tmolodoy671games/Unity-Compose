using System;
using System.Collections.Generic;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using static SharpExtensions.CustomSwitch;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    private record CoordinatorEntry(
        IComposeCoordinator? Coordinator,
        CoordinatorEntry? Parent
    );

    private static readonly ICompositionLocal<CoordinatorEntry> LocalCoordinator =
        CompositionLocalOf(() => new CoordinatorEntry(null, null));

    public static T FindCoordinator<T>()
    {
        var currentEntry = LocalCoordinator.Current;
        return Remember(() =>
        {
            while (currentEntry.Coordinator is not T && currentEntry.Parent != null)
            {
                currentEntry = currentEntry.Parent;
            }

            if (currentEntry.Coordinator is not T coordinator)
                throw new ArgumentException($"Coordinator of type {typeof(T).Name} not found!");
            return coordinator;
        });
    }

    [Composable]
    public static void Navigation(
        IComposeCoordinator coordinator,
        Func<ContentTransform>? transition = null,
        IModifier? modifier = null
    )
    {
        var initialScreens = Remember(coordinator.InitialScreens);
        var backStack = Remember(() => MutableStateListOf(initialScreens));
        var coordinatorEntry = LocalCoordinator.Current;
        var parentCoordinator = coordinatorEntry.Coordinator;
        IComposeNavigator navigator = Remember((parentCoordinator, backStack),
            () => new ComposeNavigatorImpl(backStack, parentCoordinator)
        );

        DisposableEffect(
            key: coordinator,
            effect: it =>
            {
                coordinator.CommandBuffer.SetNavigator(navigator);
                return it.OnDispose(() => coordinator.CommandBuffer.RemoveNavigator());
            }
        );

        var isSwitched = Remember(() => MutableStateOf(false));
        var currentBackStack = backStack.GetOrDefault(backStack.Count - 1, ImmutableStableListOf<ComposeScreen>());
        var previousBackStack = Remember(() =>
            MutableStablePropertyOf(initialScreens.OrEmpty().ToImmutableStableList())
        );
        LaunchedEffect(currentBackStack, () =>
        {
            if (!Equals(currentBackStack, previousBackStack.Value))
                isSwitched.Value = !isSwitched.Value;
        });

        var appearingScreens = Remember(
            (currentBackStack, previousBackStack.Value),
            () => currentBackStack
                .WhereNot(previousBackStack.Value.Contains)
                .ToImmutableStableList()
        );
        var disappearingScreens = Remember(
            (currentBackStack, previousBackStack.Value),
            () => previousBackStack.Value
                .WhereNot(currentBackStack.Contains)
                .ToImmutableStableList()
        );
        var allScreens = Remember(
            (currentBackStack, previousBackStack.Value),
            () => previousBackStack.Value
                .Union(currentBackStack)
                .Distinct()
                .OrderBy(static it => it.Priority)
                .ToImmutableStableList()
        );
        var resolvedTransition = Remember((appearingScreens, disappearingScreens, transition), () =>
            ResolveTransition(transition, appearingScreens, disappearingScreens)
        );
        var progress = AnimateFloatAsState(
            targetValue: isSwitched.Value ? 1 : 0f,
            animationSpec: Tween(
                easing: LinearEasing,
                duration: resolvedTransition.TotalDuration
            )
        ).Value;
        var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
        var isTransitionFinished = resolvedProgress.AlmostEquals(1f);
        var resolvedDuration = resolvedProgress * resolvedTransition.TotalDuration;
        var screensToRender = Remember(
            (allScreens, appearingScreens, disappearingScreens, isTransitionFinished),
            () => allScreens
                .Select(screen =>
                {
                    var screenState = Switch()
                        .Case(appearingScreens.Contains(screen), TransitionState.Entering)
                        .Case(disappearingScreens.Contains(screen), TransitionState.Exiting)
                        .Default(TransitionState.Idle)
                        .Get();
                    if (screenState == TransitionState.Entering && isTransitionFinished)
                        screenState = TransitionState.Idle;
                    return (Screen: screen, ScreenState: screenState);
                })
                .Where(it => it.ScreenState != TransitionState.Exiting || !isTransitionFinished)
                .ToImmutableStableList()
        );

        ReusableComposeView<Navigation>(
            modifier: modifier,
            content: () =>
            {
                foreach (var (screen, screenState) in screensToRender)
                {
                    Key(
                        key: screen,
                        content: () =>
                        {
                            var parent = CurrentComposer.GetParentVisualElement().NotNull();
                            var isCurrentScreen = screen.Equals(
                                currentBackStack!.GetOrDefault(currentBackStack.Count - 1, null)
                            );
                            var contentModifier = screenState switch
                            {
                                TransitionState.Idle => Modifier
                                    .Float(!isCurrentScreen),
                                TransitionState.Entering => resolvedTransition.Enter
                                    .Get(resolvedDuration, parent)
                                    .Float(!isCurrentScreen),
                                TransitionState.Exiting => resolvedTransition.Exit
                                    .Get(resolvedDuration, parent)
                                    .Float(),
                                _ => throw new ArgumentOutOfRangeException()
                            };
                            var state = TransitionResolvedState.Create(
                                state: screenState,
                                absoluteProgress: resolvedProgress,
                                duration: resolvedTransition.TotalDuration
                            );
                            var isActive = LocalIsActive.Current;
                            CompositionLocalProvider(
                                LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)),
                                LocalIsActive.Provides(
                                    new IsActiveEntry(
                                        IsActiveSelf: isCurrentScreen &&
                                                      resolvedProgress.AlmostEquals(1f),
                                        Parent: isActive
                                    )
                                ),
                                LocalTransitionState.Provides(state.State),
                                LocalTransitionProgress.Provides(state.Progress),
                                LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress),
                                LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed),
                                LocalTransitionDuration.Provides(state.Duration),
                                content: () => screen.Content(contentModifier)
                            );
                        }
                    );
                }
            }
        );
        if (isTransitionFinished)
            previousBackStack.Value = currentBackStack;
    }

    private static ContentTransform ResolveTransition(
        Func<ContentTransform>? transition,
        IStableList<ComposeScreen> enteringScreens,
        IStableList<ComposeScreen> exitingScreens
    )
    {
        if (enteringScreens.Count == 1)
        {
            var transitions = enteringScreens[0].Transitions;
            if (transitions != null)
                return transitions.Enter;
        }

        if (exitingScreens.Count == 1)
        {
            var transitions = exitingScreens[0].Transitions;
            if (transitions != null)
                return transitions.Exit;
        }

        return transition?.Invoke() ?? ContentTransform.Instant;
    }
}

public interface INavigationScope
{
    [Composable]
    void Content();
}

internal partial class NavigationScopeImpl : INavigationScope
{
    private readonly ComposableContent _content;

    public NavigationScopeImpl(ComposableContent content)
    {
        _content = content;
    }

    [Composable]
    public void Content()
    {
        _content();
    }
}

internal class ComposeNavigatorImpl : IComposeNavigator
{
    private readonly IComposeCoordinator? _parentCoordinator;
    private readonly IMutableStableList<IImmutableStableList<ComposeScreen>> _backStack;

    public ComposeNavigatorImpl(
        IMutableStableList<IImmutableStableList<ComposeScreen>> backStack,
        IComposeCoordinator? parentCoordinator
    )
    {
        _backStack = backStack;
        _parentCoordinator = parentCoordinator;
    }

    public void ApplyCommands(IEnumerable<ComposeNavigationCommand> commands)
    {
        foreach (var command in commands)
            ApplyCommand(command);
    }

    private void ApplyCommand(ComposeNavigationCommand command)
    {
        switch (command)
        {
            case ComposeNavigationCommand.Back:
                if (_backStack.IsNotEmpty())
                    _backStack.RemoveAt(_backStack.Count - 1);
                else
                    _parentCoordinator?.GoBack();
                break;
            case ComposeNavigationCommand.BackTo backTo:
                if (backTo.Screen == null)
                {
                    _backStack.Clear();
                    break;
                }

                while (_backStack.IsNotEmpty() &&
                       _backStack[^1].None(it => it.ScreenKey == backTo.Screen.ScreenKey))
                    _backStack.RemoveAt(_backStack.Count - 1);
                break;
            case ComposeNavigationCommand.Forward forward:
                var currentStack = _backStack.GetOrDefault(
                    _backStack.Count - 1,
                    IImmutableStableList.Empty<ComposeScreen>()
                );
                _backStack.Add(currentStack.Add(forward.Screen));
                break;
            case ComposeNavigationCommand.Replace replace:
                _backStack.Add(IImmutableStableList.Create(replace.Screen));
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(command));
        }
    }
}