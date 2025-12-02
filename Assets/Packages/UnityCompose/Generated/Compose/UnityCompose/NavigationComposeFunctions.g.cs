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
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1768390015);
        if (__composer.ShouldExecute((__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier)))
        {
            var backStack = !__composer.RememberedKeyChanged<bool>(-122087210, true) ? __composer.RememberedValue<UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(MutableStateListOf(initialScreens.OrEmpty().ToImmutableStableList()));
            var coordinatorEntry = LocalCoordinator.Current;
            var parentCoordinator = coordinatorEntry.Coordinator;
            IComposeNavigator navigator = !__composer.RememberedKeyChanged<(UnityCompose.IComposeCoordinator? parentCoordinator, UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>> backStack)>(-412119540, (parentCoordinator, backStack)) ? __composer.RememberedValue<UnityCompose.ComposeNavigatorImpl>() : __composer.UpdateRememberedValue<UnityCompose.ComposeNavigatorImpl>(new ComposeNavigatorImpl(backStack, parentCoordinator));
            DisposableEffect(key: coordinator, effect: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.IComposeCoordinator, UnityCompose.IComposeNavigator>>(-1578792728, (coordinator, navigator)) ? CurrentComposer.RememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : CurrentComposer.UpdateLambda<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
            {
                coordinator.CommandBuffer.SetNavigator(navigator);
                return it.OnDispose(!__composer.RememberedKeyChanged<UnityCompose.IComposeCoordinator>(-407220374, coordinator) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => coordinator.CommandBuffer.RemoveNavigator()));
            }));
            var isSwitched = !__composer.RememberedKeyChanged<bool>(-525124656, true) ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
            var currentBackStack = backStack.GetOrDefault(backStack.Count - 1, IImmutableStableList.Empty<ComposeScreen>());
            var previousBackStack = !__composer.RememberedKeyChanged<bool>(229066550, true) ? __composer.RememberedValue<StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(IMutableStableProperty.Create(initialScreens.OrEmpty().ToImmutableStableList()));
            LaunchedEffect(currentBackStack, !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.IMutableState<bool>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>?>>(-1405182596, (isSwitched, currentBackStack, previousBackStack)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
            {
                if (!Equals(currentBackStack, previousBackStack.Value))
                    isSwitched.Value = !isSwitched.Value;
            }));
            var appearingScreens = !__composer.RememberedKeyChanged<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value)>(618259719, (currentBackStack, previousBackStack.Value)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(currentBackStack.WhereNot(previousBackStack.Value.Contains).ToImmutableStableList());
            var disappearingScreens = !__composer.RememberedKeyChanged<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value)>(-1099612885, (currentBackStack, previousBackStack.Value)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(previousBackStack.Value.WhereNot(currentBackStack.Contains).ToImmutableStableList());
            var allScreens = !__composer.RememberedKeyChanged<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value)>(1710090918, (currentBackStack, previousBackStack.Value)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(previousBackStack.Value.Union(currentBackStack).Distinct().OrderBy(static it => it.Priority).ToImmutableStableList());
            var resolvedTransition = !__composer.RememberedKeyChanged<(StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> appearingScreens, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> disappearingScreens, System.Func<UnityCompose.ContentTransform>? transition)>(1474599597, (appearingScreens, disappearingScreens, transition)) ? __composer.RememberedValue<UnityCompose.ContentTransform>() : __composer.UpdateRememberedValue<UnityCompose.ContentTransform>(ResolveTransition(transition, appearingScreens, disappearingScreens));
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: resolvedTransition.TotalDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var isTransitionFinished = resolvedProgress.AlmostEquals(1f);
            var resolvedDuration = resolvedProgress * resolvedTransition.TotalDuration;
            ReusableComposeView<Navigation>(modifier: modifier, content: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.IComposeCoordinator, System.Action<float>?, System.Action<UnityCompose.INavigationScope>?, UnityCompose.ComposeFunctions.CoordinatorEntry?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, ValueTuple<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, UnityCompose.ContentTransform, float, bool, float>>>(373609324, (coordinator, onTransitionProgressChanged, content, coordinatorEntry, currentBackStack, previousBackStack, appearingScreens, disappearingScreens, allScreens, resolvedTransition, resolvedProgress, isTransitionFinished, resolvedDuration)) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent?>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent?>(() =>
            {
                LaunchedEffect(resolvedProgress, !__composer.RememberedKeyChanged<ValueTuple<System.Action<float>?, float>>(-1820451197, (onTransitionProgressChanged, resolvedProgress)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() => onTransitionProgressChanged?.Invoke(resolvedProgress)));
                if (IsInPreview)
                    return;
                __composer.StartReplaceGroup(427855032);
                foreach (var screen in allScreens)
                {
                    var screenState = !__composer.RememberedKeyChanged<(UnityCompose.ComposeScreen screen, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> currentBackStack, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen> Value)>(-1871528917, (screen, currentBackStack, previousBackStack.Value)) ? __composer.RememberedValue<UnityCompose.TransitionState>() : __composer.UpdateRememberedValue<UnityCompose.TransitionState>(Switch().Case(appearingScreens.Contains(screen), TransitionState.Entering).Case(disappearingScreens.Contains(screen), TransitionState.Exiting).Default(TransitionState.Idle).Get());
                    if (screenState == TransitionState.Exiting && isTransitionFinished)
                        continue;
                    if (screenState == TransitionState.Entering && isTransitionFinished)
                        screenState = TransitionState.Idle;
                    Key(key: screen, content: !__composer.RememberedKeyChanged<ValueTuple<UnityCompose.IComposeCoordinator, System.Action<UnityCompose.INavigationScope>?, UnityCompose.ComposeFunctions.CoordinatorEntry?, StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>?, UnityCompose.ContentTransform, float, float, ValueTuple<UnityCompose.ComposeScreen?, UnityCompose.TransitionState>>>(1574762525, (coordinator, content, coordinatorEntry, currentBackStack, resolvedTransition, resolvedProgress, resolvedDuration, screen, screenState)) ? CurrentComposer.RememberedValue<System.Action>() : CurrentComposer.UpdateLambda<System.Action>(() =>
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
                        var scope = !__composer.RememberedKeyChanged<UnityCompose.ComposeScreen>(-219983774, screen) ? __composer.RememberedValue<UnityCompose.NavigationScopeImpl>() : __composer.UpdateRememberedValue<UnityCompose.NavigationScopeImpl>(new NavigationScopeImpl(screen.Content));
                        CompositionLocalProvider(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)), LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: isActive)), LocalModifier.Provides(after: LocalModifier.Current.After.OrEmpty().Then(contentModifier)), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: !__composer.RememberedKeyChanged<ValueTuple<System.Action<UnityCompose.INavigationScope>?, UnityCompose.NavigationScopeImpl?>>(-1375352465, (content, scope)) ? CurrentComposer.RememberedValue<UnityCompose.ComposableContent>() : CurrentComposer.UpdateComposableLambda<UnityCompose.ComposableContent>(() =>
                        {
                            if (content != null)
                            {
                                __composer.StartReplaceGroup(1614771844);
                                content(scope);
                                __composer.EndReplaceGroup(1614771844);
                            }
                            else
                            {
                                __composer.StartReplaceGroup(870126151);
                                scope.Content();
                                __composer.EndReplaceGroup(870126151);
                            }
                        }));
                    }));
                }

                __composer.EndReplaceGroup(427855032);
            }));
            if (isTransitionFinished)
                previousBackStack.Value = currentBackStack;
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1768390015)?.UpdateScope(() => __Navigation(__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier));
    }
}

internal partial class NavigationScopeImpl
{
    [Composable]
    private void __Content()
    {
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(1960824537);
        if (__composer.ShouldExecute(true))
        {
            _content();
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(1960824537)?.UpdateScope(() => __Content());
    }
}