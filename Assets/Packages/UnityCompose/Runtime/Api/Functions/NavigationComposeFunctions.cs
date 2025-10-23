using System;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
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
        float transitionDuration = ComposeDefaults.TransitionDuration,
        AnimationCurve? animationCurve = null,
        IImmutableStableList<ComposeScreen>? initialScreens = null,
        Action<float>? onTransitionProgressChanged = null,
        IModifier? style = null
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

        var progress = AnimateFloatAsState(
            targetValue: isSwitched.Value ? 1 : 0f,
            duration: transitionDuration,
            animationCurve: animationCurve
        ).Value;
        var resolvedProgress = isSwitched.Value ? progress : 1 - progress;

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
            transition?.Invoke() ?? ResolveTransition(appearingScreens, disappearingScreens)
        );
        var isTransitionFinished = resolvedProgress.AlmostEquals(1f);

        ReusableComposeView<Navigation>(
            style: style,
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
                                    .Case(appearingScreens.Contains(screen), ScreenState.Appearing)
                                    .Case(disappearingScreens.Contains(screen), ScreenState.Disappearing)
                                    .Default(ScreenState.Idle)
                                    .Get()
                            );
                            if (screenState == ScreenState.Disappearing && isTransitionFinished)
                                continue;
                            Key(
                                key: screen,
                                content: () =>
                                {
                                    var parent = LocalVisualElement.Current;
                                    var isCurrentScreen = screen.Equals(currentBackStack[^1]);
                                    var contentStyle = screenState switch
                                    {
                                        ScreenState.Idle => IModifier.Empty
                                            .Position(isCurrentScreen ? Position.Relative : Position.Absolute),
                                        ScreenState.Appearing => resolvedTransition.Enter
                                            .Get(resolvedProgress, parent)
                                            .Position(isCurrentScreen ? Position.Relative : Position.Absolute),
                                        ScreenState.Disappearing => resolvedTransition.Exit
                                            .Get(resolvedProgress, parent)
                                            .Position(Position.Absolute),
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
                                            LocalStyle.Provides(
                                                after: LocalStyle.Current.After.OrEmpty()
                                                    .Then(contentStyle)
                                            ),
                                            LocalTransitionProgress.Provides(
                                                screenState != ScreenState.Disappearing
                                                    ? resolvedProgress
                                                    : 1 - resolvedProgress
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

    private enum ScreenState
    {
        Idle,
        Appearing,
        Disappearing,
    }

    private static ContentTransform ResolveTransition(
        IStableList<ComposeScreen> enteringScreens,
        IStableList<ComposeScreen> exitingScreens
    )
    {
        if (enteringScreens.Count == 1)
            return enteringScreens[0].Transitions.Enter;
        if (exitingScreens.Count == 1)
            return exitingScreens[0].Transitions.Exit;
        return InstantContentTransform;
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