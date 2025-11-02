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
            () => currentBackStack
                .Union(previousBackStack.Value)
                .Distinct()
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
                CompositionLocalProvider(
                    provides: Remember(() => IImmutableStableList.Create(
                        LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry))
                    )),
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
                            Key(
                                key: screen,
                                content: () =>
                                {
                                    var parent = LocalParentLayout.Current;
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
                                    CompositionLocalProvider(
                                        provides: IImmutableStableList.Create(
                                            LocalIsActive.Provides(
                                                new IsActiveEntry(
                                                    IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f),
                                                    Parent: LocalIsActive.Current
                                                )
                                            ),
                                            LocalModifier.Provides(
                                                after: LocalModifier.Current.After.OrEmpty()
                                                    .Then(contentStyle)
                                            ),
                                            LocalTransitionState.Provides(
                                                TransitionState.Create(
                                                    state: screenState,
                                                    absoluteProgress: resolvedProgress,
                                                    duration: resolvedDuration
                                                )
                                            )
                                        ),
                                        content: screen.Content
                                    );
                                }
                            );
                        }
                    }
                );
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
        if (transition != null)
            return transition();
        if (enteringScreens.Count == 1)
            return enteringScreens[0].Transitions.Enter;
        if (exitingScreens.Count == 1)
            return exitingScreens[0].Transitions.Exit;
        return ContentTransform.Instant;
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