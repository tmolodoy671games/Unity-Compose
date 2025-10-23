using System;
using System.Linq;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
using static SharpExtensions.CustomSwitch;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    [Compiled]
    private static void __Navigation(IComposeCoordinator coordinator, Func<ContentTransform>? transition = null, float transitionDuration = ComposeDefaults.TransitionDuration, AnimationCurve? animationCurve = null, IImmutableStableList<ComposeScreen>? initialScreens = null, Action<float>? onTransitionProgressChanged = null, IModifier? style = null)
    {
        if (CurrentComposer.BeginComposeGroup((coordinator, transition, transitionDuration, animationCurve, initialScreens, onTransitionProgressChanged, style)))
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
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, duration: transitionDuration, animationCurve: animationCurve).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var appearingScreens = Remember((currentBackStack, previousBackStack.Value), () => currentBackStack.WhereNot(previousBackStack.Value.Contains).ToImmutableStableList());
            var disappearingScreens = Remember((currentBackStack, previousBackStack.Value), () => previousBackStack.Value.WhereNot(currentBackStack.Contains).ToImmutableStableList());
            var allScreens = Remember((currentBackStack, previousBackStack.Value), () => currentBackStack.Union(previousBackStack.Value).Distinct().ToImmutableStableList());
            var resolvedTransition = Remember((appearingScreens, disappearingScreens, transition), () => transition?.Invoke() ?? ResolveTransition(appearingScreens, disappearingScreens));
            var isTransitionFinished = resolvedProgress.AlmostEquals(1f);
            ReusableComposeView<Navigation>(style: style, content: RememberComposable<global::System.Action>((coordinator, onTransitionProgressChanged, coordinatorEntry, currentBackStack, previousBackStack, resolvedProgress, appearingScreens, disappearingScreens, allScreens, resolvedTransition, isTransitionFinished), () =>
            {
                CompositionLocalProvider(provides: Remember(() => IImmutableStableList.Create(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)))), content: RememberComposable<global::System.Action>((onTransitionProgressChanged, currentBackStack, previousBackStack, resolvedProgress, appearingScreens, disappearingScreens, allScreens, resolvedTransition, isTransitionFinished), () =>
                {
                    LaunchedEffect(resolvedProgress, Remember<global::System.Action>((onTransitionProgressChanged, resolvedProgress), () => onTransitionProgressChanged?.Invoke(resolvedProgress)));
                    if (IsInPreview)
                        return;
                    foreach (var screen in allScreens)
                    {
                        var screenState = Remember((screen, currentBackStack, previousBackStack.Value), () => Switch().Case(appearingScreens.Contains(screen), ScreenState.Appearing).Case(disappearingScreens.Contains(screen), ScreenState.Disappearing).Default(ScreenState.Idle).Get());
                        if (screenState == ScreenState.Disappearing && isTransitionFinished)
                            continue;
                        Key(key: screen, content: RememberComposable<global::System.Action>((currentBackStack, resolvedProgress, resolvedTransition, screen, screenState), () =>
                        {
                            var parent = LocalVisualElement.Current;
                            var isCurrentScreen = screen.Equals(currentBackStack[^1]);
                            var contentStyle = screenState switch
                            {
                                ScreenState.Idle => Modifier.Float(!isCurrentScreen),
                                ScreenState.Appearing => resolvedTransition.Enter.Get(resolvedProgress, parent).Float(!isCurrentScreen),
                                ScreenState.Disappearing => resolvedTransition.Exit.Get(resolvedProgress, parent).Float(),
                                _ => throw new ArgumentOutOfRangeException()};
                            CompositionLocalProvider(provides: IImmutableStableList.Create(LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: LocalIsActive.Current)), LocalStyle.Provides(after: LocalStyle.Current.After.OrEmpty().Then(contentStyle)), LocalTransitionProgress.Provides(screenState != ScreenState.Disappearing ? resolvedProgress : 1 - resolvedProgress)), content: screen.Content);
                        }));
                    }
                }));
            }));
            if (isTransitionFinished)
                previousBackStack.Value = currentBackStack;
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __Navigation(coordinator, transition, transitionDuration, animationCurve, initialScreens, onTransitionProgressChanged, style));
        }
    }
}