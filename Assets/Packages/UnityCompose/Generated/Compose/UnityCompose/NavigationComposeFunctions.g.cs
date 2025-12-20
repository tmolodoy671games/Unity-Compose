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
        __composer.StartRestartGroup(261249727);
        var __isRestarted = __composer.IsRestarted();
        if (__composer.ShouldExecuteAsStruct((__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier)))
        {
            var backStack = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableStateList<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(MutableStateListOf(initialScreens.OrEmpty().ToImmutableStableList()));
            var coordinatorEntry = LocalCoordinator.Current;
            var parentCoordinator = coordinatorEntry.Coordinator;
            IComposeNavigator navigator = !__composer.ChangedAsStruct((parentCoordinator, backStack)) ? __composer.RememberedValue<UnityCompose.ComposeNavigatorImpl>() : __composer.UpdateRememberedValue<UnityCompose.ComposeNavigatorImpl>(new ComposeNavigatorImpl(backStack, parentCoordinator));
            DisposableEffect(key: coordinator, effect: !__composer.ChangedAsStruct((coordinator, navigator)) ? __composer.RememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>() : __composer.UpdateRememberedValue<System.Func<UnityCompose.IDisposableEffectScope, System.IDisposable>>(it =>
            {
                coordinator.CommandBuffer.SetNavigator(navigator);
                return it.OnDispose(() => coordinator.CommandBuffer.RemoveNavigator());
            }));
            var isSwitched = !__composer.Changed() ? __composer.RememberedValue<UnityCompose.IMutableState<bool>>() : __composer.UpdateRememberedValue<UnityCompose.IMutableState<bool>>(MutableStateOf(false));
            var currentBackStack = backStack.GetOrDefault(backStack.Count - 1, IImmutableStableList.Empty<ComposeScreen>());
            var previousBackStack = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>>(IMutableStableProperty.Create(initialScreens.OrEmpty().ToImmutableStableList()));
            LaunchedEffect(currentBackStack, !__composer.ChangedAsStruct((isSwitched, currentBackStack, previousBackStack)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
            {
                if (!Equals(currentBackStack, previousBackStack.Value))
                    isSwitched.Value = !isSwitched.Value;
            }));
            var appearingScreens = !__composer.ChangedAsStruct((currentBackStack, previousBackStack.Value)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(currentBackStack.WhereNot(previousBackStack.Value.Contains).ToImmutableStableList());
            var disappearingScreens = !__composer.ChangedAsStruct((currentBackStack, previousBackStack.Value)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(previousBackStack.Value.WhereNot(currentBackStack.Contains).ToImmutableStableList());
            var allScreens = !__composer.ChangedAsStruct((currentBackStack, previousBackStack.Value)) ? __composer.RememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>() : __composer.UpdateRememberedValue<StableCollections.IImmutableStableList<UnityCompose.ComposeScreen>>(previousBackStack.Value.Union(currentBackStack).Distinct().OrderBy(static it => it.Priority).ToImmutableStableList());
            var resolvedTransition = !__composer.ChangedAsStruct((appearingScreens, disappearingScreens, transition)) ? __composer.RememberedValueAsStruct<UnityCompose.ContentTransform>() : __composer.UpdateRememberedValue<UnityCompose.ContentTransform>(ResolveTransition(transition, appearingScreens, disappearingScreens));
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: resolvedTransition.TotalDuration)).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var isTransitionFinished = resolvedProgress.AlmostEquals(1f);
            var resolvedDuration = resolvedProgress * resolvedTransition.TotalDuration;
            ReusableComposeView<Navigation>(modifier: modifier, content: !__composer.ChangedAsStruct((coordinator, onTransitionProgressChanged, content, coordinatorEntry, currentBackStack, previousBackStack, appearingScreens, disappearingScreens, allScreens, resolvedTransition, resolvedProgress, isTransitionFinished, resolvedDuration)) ? __composer.RememberedValue<UnityCompose.ComposableContent?>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent?>(() =>
            {
                LaunchedEffect(resolvedProgress, !__composer.ChangedAsStruct((onTransitionProgressChanged, resolvedProgress)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => onTransitionProgressChanged?.Invoke(resolvedProgress)));
                if (IsInPreview)
                    return;
                __composer.StartReplaceGroup(-1873039072);
                foreach (var screen in allScreens)
                {
                    var screenState = !__composer.ChangedAsStruct((screen, currentBackStack, previousBackStack.Value)) ? __composer.RememberedValueAsStruct<UnityCompose.TransitionState>() : __composer.UpdateRememberedValue<UnityCompose.TransitionState>(Switch().Case(appearingScreens.Contains(screen), TransitionState.Entering).Case(disappearingScreens.Contains(screen), TransitionState.Exiting).Default(TransitionState.Idle).Get());
                    if (screenState == TransitionState.Exiting && isTransitionFinished)
                        continue;
                    if (screenState == TransitionState.Entering && isTransitionFinished)
                        screenState = TransitionState.Idle;
                    Key(key: screen, content: !__composer.ChangedAsStruct((coordinator, content, coordinatorEntry, currentBackStack, resolvedTransition, resolvedProgress, resolvedDuration, screen, screenState)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                    {
                        var parent = CurrentComposer.GetParentVisualElement().NotNull();
                        var isCurrentScreen = screen.Equals(currentBackStack[^1]);
                        var contentModifier = screenState switch
                        {
                            TransitionState.Idle => Modifier.Float(!isCurrentScreen),
                            TransitionState.Entering => resolvedTransition.Enter.Get(resolvedDuration, parent).Float(!isCurrentScreen),
                            TransitionState.Exiting => resolvedTransition.Exit.Get(resolvedDuration, parent).Float(),
                            _ => throw new ArgumentOutOfRangeException()};
                        var state = TransitionResolvedState.Create(state: screenState, absoluteProgress: resolvedProgress, duration: resolvedTransition.TotalDuration);
                        var isActive = LocalIsActive.Current;
                        var scope = !__composer.Changed(screen) ? __composer.RememberedValue<UnityCompose.NavigationScopeImpl>() : __composer.UpdateRememberedValue<UnityCompose.NavigationScopeImpl>(new NavigationScopeImpl(screen.Content));
                        CompositionLocalProvider(LocalCoordinator.Provides(new CoordinatorEntry(coordinator, coordinatorEntry)), LocalIsActive.Provides(new IsActiveEntry(IsActiveSelf: isCurrentScreen && resolvedProgress.AlmostEquals(1f), Parent: isActive)), LocalTransitionState.Provides(state.State), LocalTransitionProgress.Provides(state.Progress), LocalTransitionAbsoluteProgress.Provides(state.AbsoluteProgress), LocalTransitionAbsoluteTimeElapsed.Provides(state.AbsoluteTimeElapsed), LocalTransitionDuration.Provides(state.Duration), content: !__composer.ChangedAsStruct((content, contentModifier, scope)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                        {
                            WithModifiers(after: CurrentComposer.GetModifiers().After.OrEmpty().Then(contentModifier), content: !__composer.ChangedAsStruct((content, scope)) ? __composer.RememberedValue<UnityCompose.ComposableContent>() : __composer.UpdateRememberedValue<UnityCompose.ComposableContent>(() =>
                            {
                                if (content != null)
                                {
                                    __composer.StartReplaceGroup(57981188);
                                    content(scope);
                                    __composer.EndReplaceGroup(57981188);
                                }
                                else
                                {
                                    __composer.StartReplaceGroup(-322363526);
                                    scope.Content();
                                    __composer.EndReplaceGroup(-322363526);
                                }
                            }));
                        }));
                    }));
                }

                __composer.EndReplaceGroup(-1873039072);
            }));
            if (isTransitionFinished)
                previousBackStack.Value = currentBackStack;
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(261249727, __isRestarted)?.UpdateScope(() => __Navigation(__coordinator, __transition, __initialScreens, __onTransitionProgressChanged, __content, __modifier));
    }
}

internal partial class NavigationScopeImpl
{
    [Composable]
    private void __Content()
    {
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(606385123);
        var __isRestarted = __composer.IsRestarted();
        if (__composer.ShouldExecute())
        {
            _content();
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(606385123, __isRestarted)?.UpdateScope(() => __Content());
    }
}