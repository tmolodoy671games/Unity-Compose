using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using SharpExtensions;
using UnityEngine;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    private static void __AnimatedContent<T>(T targetState, Func<IAnimatedContentTransitionScope<T>, ContentTransform> transitionSpec, ComposableContent<T> content, Optional<AnimationSpec> sizeAnimationSpec = default, IModifier? modifier = null)
    {
        var(__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier) = (targetState, transitionSpec, content, sizeAnimationSpec, modifier);
        var __composer = CurrentComposer;
        __composer.StartRestartGroup(497003188);
        var __isRestarted = __composer.IsRestarted();
        if (__isRestarted || __composer.ShouldExecuteAsStruct((__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier)))
        {
            var isSwitched = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<bool>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<bool>>(IMutableStableProperty.Create(false));
            LaunchedEffect(targetState, !__composer.Changed(isSwitched) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() => isSwitched.Value = !isSwitched.Value));
            var previousValue = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<T>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<T>>(IMutableStableProperty.Create(targetState));
            var targetValue = !__composer.Changed() ? __composer.RememberedValue<StableCollections.IMutableStableProperty<T>>() : __composer.UpdateRememberedValue<StableCollections.IMutableStableProperty<T>>(IMutableStableProperty.Create(targetState));
            LaunchedEffect(targetState, !__composer.ChangedAsStruct((targetState, previousValue, targetValue)) ? __composer.RememberedValue<System.Action>() : __composer.UpdateRememberedValue<System.Action>(() =>
            {
                previousValue.Value = targetValue.Value;
                targetValue.Value = targetState;
            }));
            var resolvedTransition = !__composer.Changed(targetState!) ? __composer.RememberedValueAsStruct<UnityCompose.ContentTransform>() : __composer.UpdateRememberedValue<UnityCompose.ContentTransform>(Equals(previousValue.Value, targetState) ? IEnterTransition.Empty().TogetherWith(Hide()) : transitionSpec(new AnimatedContentTransitionScopeImpl<T>(previousValue.Value, targetState)));
            var transitionDuration = resolvedTransition.TotalDuration;
            var progress = AnimateFloatAsState(targetValue: isSwitched.Value ? 1 : 0f, animationSpec: Tween(easing: LinearEasing, duration: transitionDuration)).Value;
        }
        else
        {
            __composer.SkipToGroupEnd();
        }

        __composer.EndRestartGroup(497003188, __isRestarted)?.UpdateScope(() => __AnimatedContent(__targetState, __transitionSpec, __content, __sizeAnimationSpec, __modifier));
    }
}