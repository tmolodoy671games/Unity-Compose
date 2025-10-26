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
    private static void __Navigation(IComposeCoordinator coordinator, Func<ContentTransform>? transition = null, IImmutableStableList<ComposeScreen>? initialScreens = null, Action<float>? onTransitionProgressChanged = null, IModifier? modifier = null)
    {
        if (CurrentComposer.BeginComposeGroup((coordinator, transition, initialScreens, onTransitionProgressChanged, modifier)))
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
            var allScreens = Remember((currentBackStack, previousBackStack.Value), () => currentBackStack.Union(previousBackStack.Value).Distinct().ToImmutableStableList());
            var resolvedTransition = Remember((appearingScreens, disappearingScreens, transition), () => transition?.Invoke() ?? ResolveTransition(appearingScreens, disappearingScreens));
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: resolvedTransition.TotalDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var isTransitionFinished = resolvedProgress.AlmostEquals(1f);
            var resolvedDuration = resolvedProgress * resolvedTransition.TotalDuration;
            ReusableComposeView<Navigation>(modifier: modifier, content: RememberComposable<global::System.Action>((coordinator, onTransitionProgressChanged, coordinatorEntry, currentBackStack, previousBackStack, appearingScreens, disappearingScreens, allScreens, resolvedTransition, resolvedProgress, isTransitionFinished, resolvedDuration), () =>
            {
                CompositionLocalProvider(provides: Remember(() => IImmutableStableList.Create(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)))), content: RememberComposable<global::System.Action>((onTransitionProgressChanged, currentBackStack, previousBackStack, appearingScreens, disappearingScreens, allScreens, resolvedTransition, resolvedProgress, isTransitionFinished, resolvedDuration), () =>
                {
                    LaunchedEffect(resolvedProgress, Remember<global::System.Action>((onTransitionProgressChanged, resolvedProgress), () => onTransitionProgressChanged?.Invoke(resolvedProgress)));
                    if (IsInPreview)
                        return;
                    foreach (var screen in allScreens)
                    {
                        var screenState = Remember((screen, currentBackStack, previousBackStack.Value), () => Switch().Case(appearingScreens.Contains(screen), ContentState.Entering).Case(disappearingScreens.Contains(screen), ContentState.Exiting).Default(ContentState.Idle).Get());
                        if (screenState == ContentState.Exiting && isTransitionFinished)
                            continue;
                        Key(key: screen, content: RememberComposable<global::System.Action>((currentBackStack, resolvedTransition, resolvedProgress, resolvedDuration, screen, screenState), () =>
                        {
                            var parent = LocalParentLayout.Current;
                            var isCurrentScreen = screen.Equals(currentBackStack[^1]);
                            var contentStyle = screenState switch
                            {
                                ContentState.Idle => Modifier.Float(!isCurrentScreen),
                                ContentState.Entering => resolvedTransition.Enter.Get(resolvedDuration, parent).Float(!isCurrentScreen),
                                ContentState.Exiting => resolvedTransition.Exit.Get(resolvedDuration, parent).Float(),
                                _ => throw new ArgumentOutOfRangeException()};
                            CompositionLocalProvider(provides: IImmutableStableList.Create(LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: LocalIsActive.Current)), LocalModifier.Provides(after: LocalModifier.Current.After.OrEmpty().Then(contentStyle)), LocalTransitionProgress.Provides(screenState != ContentState.Exiting ? resolvedProgress : 1 - resolvedProgress)), content: screen.Content);
                        }));
                    }
                }));
            }));
            if (isTransitionFinished)
                previousBackStack.Value = currentBackStack;
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Navigation(coordinator, transition, initialScreens, onTransitionProgressChanged, modifier));
        }
    }
}