using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    [Compiled]
    private static void __AnimatedContent<T>(T value, Func<T, T, ContentTransform> transition, [Composable] Action<T> content, bool animateSize = false, float transitionDuration = ComposeDefaults.TransitionDuration, IModifier? style = null)
    {
        if (CurrentComposer.BeginComposeGroup((value, transition, content, animateSize, transitionDuration, style)))
            return;
        try
        {
            // Progress:
            var isSwitched = Remember(() => MutableStateOf(false));
            LaunchedEffect(value!, Remember<global::System.Action>(isSwitched, () => isSwitched.Value = !isSwitched.Value));
            var progress = AnimateFloatAsState(isSwitched.Value ? 1 : 0f, transitionDuration).Value;
            var resolvedProgress = isSwitched.Value ? progress : 1 - progress;
            var previousValue = Remember(() => IMutableStableProperty.Create(value));
            var targetValue = Remember(() => IMutableStableProperty.Create(value));
            LaunchedEffect(value!, Remember<global::System.Action>((value, previousValue, targetValue), () =>
            {
                previousValue.Value = targetValue.Value;
                targetValue.Value = value;
            }));
            // Animating size:
            var(containerStyle, contentStyle) = animateSize ? AnimateSizeStyles(transitionDuration) : (Modifier, Modifier);
            // Layout:
            var resolvedTransition = Remember(value!, () => Equals(previousValue.Value, value) ? ContentTransform() : transition(previousValue.Value, value));
            ReusableComposeView<AnimatedContent>(style: style.OrEmpty().Then(containerStyle), content: RememberComposable<global::System.Action>((value, content, isSwitched, resolvedProgress, previousValue, contentStyle, resolvedTransition), () =>
            {
                var parent = LocalVisualElement.Current;
                var nextStyle = resolvedTransition.Enter.Get(resolvedProgress, parent).Then(contentStyle);
                var previousStyle = resolvedTransition.Exit.Get(resolvedProgress, parent).Position(Position.Absolute);
                var isAnimationRunning = resolvedProgress is> 0 and < 1;
                var next = (Value: value, Style: nextStyle, Progress: resolvedProgress);
                var previous = (Value: previousValue.Value, Style: previousStyle, Progress: 1 - resolvedProgress);
                var pair = isSwitched.Value ? (First: next, Second: previous) : (First: previous, Second: next);
                if (isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "First", content: RememberComposable<global::System.Action>((content, pair), () =>
                    {
                        CompositionLocalProvider(provides: IImmutableStableList.Create(LocalStyle.Provides(after: pair.First.Style), LocalTransitionProgress.Provides(pair.First.Progress)), content: RememberComposable<global::System.Action>((content, pair), () => content(pair.First.Value)));
                    }));
                }

                if (!isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "Second", content: RememberComposable<global::System.Action>((content, pair), () =>
                    {
                        CompositionLocalProvider(provides: IImmutableStableList.Create(LocalStyle.Provides(after: pair.Second.Style), LocalTransitionProgress.Provides(pair.Second.Progress)), content: RememberComposable<global::System.Action>((content, pair), () => content(pair.Second.Value)));
                    }));
                }
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __AnimatedContent<T>(value, transition, content, animateSize, transitionDuration, style));
        }
    }
}