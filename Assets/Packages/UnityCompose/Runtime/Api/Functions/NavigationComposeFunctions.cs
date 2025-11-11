using System;
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
        IImmutableStableList<ComposeScreen>? initialScreens = null,
        Action<float>? onTransitionProgressChanged = null,
        Action<INavigationScope>? content = null,
        IModifier? modifier = null
    )
    {
        var backStack = Remember(() => MutableStateListOf(initialScreens.OrEmpty().ToImmutableStableList()));
        var coordinatorEntry = LocalCoordinator.Current;
        var parentCoordinator = coordinatorEntry.Coordinator;
        var navigator = Remember<IComposeNavigator>((parentCoordinator, backStack),
            () => new ComposeNavigatorImpl(backStack, parentCoordinator)
        );

        DisposableEffect(
            key: coordinator,
            body: it =>
            {
                coordinator.CommandBuffer.SetNavigator(navigator);
                return it.OnDispose(() => coordinator.CommandBuffer.RemoveNavigator());
            }
        );

        var isSwitched = Remember(() => MutableStateOf(false));
        var currentBackStack = backStack.GetOrDefault(backStack.Count - 1, IImmutableStableList.Empty<ComposeScreen>());
        var previousBackStack = Remember(() =>
            IMutableStableProperty.Create(initialScreens.OrEmpty().ToImmutableStableList())
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

        ReusableComposeView<Navigation>(
            modifier: modifier,
            content: () =>
            {
                LaunchedEffect(resolvedProgress, () => onTransitionProgressChanged?.Invoke(resolvedProgress));
                if (IsInPreview)
                    return;
                foreach (var screen in allScreens)
                {
                    var screenState = Remember((screen, currentBackStack, previousBackStack.Value),
                        () => Switch()
                            .Case(appearingScreens.Contains(screen), ContentState.Entering)
                            .Case(disappearingScreens.Contains(screen), ContentState.Exiting)
                            .Default(ContentState.Idle)
                            .Get()
                    );
                    if (screenState == ContentState.Exiting && isTransitionFinished)
                        continue;
                    if (screenState == ContentState.Entering && isTransitionFinished)
                        screenState = ContentState.Idle;
                    Key(
                        key: screen,
                        content: () =>
                        {
                            var parent = LocalVisualElement.Current;
                            var isCurrentScreen = screen.Equals(currentBackStack[^1]);
                            var contentStyle = screenState switch
                            {
                                ContentState.Idle => Modifier
                                    .Float(!isCurrentScreen),
                                ContentState.Entering => resolvedTransition.Enter
                                    .Get(resolvedDuration, parent)
                                    .Float(!isCurrentScreen),
                                ContentState.Exiting => resolvedTransition.Exit
                                    .Get(resolvedDuration, parent)
                                    .Float(),
                                _ => throw new ArgumentOutOfRangeException()
                            };
                            var state = TransitionState.Create(
                                state: screenState,
                                absoluteProgress: resolvedProgress,
                                duration: resolvedTransition.TotalDuration
                            );
                            var isActive = LocalIsActive.Current;
                            var scope = Remember(screen, () => new NavigationScopeImpl(screen.Content));
                            CompositionLocalProvider(
                                LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)),
                                LocalIsActive.Provides(
                                    new IsActiveEntry(
                                        IsActiveSelf: isCurrentScreen &&
                                                      resolvedProgress.AlmostEquals(1f),
                                        Parent: isActive
                                    )
                                ),
                                LocalModifier.Provides(
                                    after: LocalModifier.Current.After.OrEmpty()
                                        .Then(contentStyle)
                                ),
                                LocalContentState.Provides(state.State),
                                LocalTransitionProgress.Provides(state.Progress),
                                LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress),
                                LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed),
                                LocalTransitionDuration.Provides(state.Duration),
                                content: () =>
                                {
                                    if (content != null)
                                        content(scope);
                                    else
                                        scope.Content();
                                }
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

internal partial class NavigationScopeImpl : INavigationScope, IEquatable<NavigationScopeImpl>
{
    [Composable] private readonly Action _content;

    public NavigationScopeImpl(Action content)
    {
        _content = content;
    }

    [Composable]
    public void Content()
    {
        _content();
    }

    public bool Equals(NavigationScopeImpl other)
    {
        return _content.Equals(other._content);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NavigationScopeImpl)obj);
    }

    public override int GetHashCode()
    {
        return _content.GetHashCode();
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

    public void ApplyCommands(IStableList<ComposeNavigationCommand> commands)
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