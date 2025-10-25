using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;
public static partial class ComposeFunctions
{
    [Composable]
    [Compiled]
    private static void __AnimatedContent<T>(T value, Func<T, T, ContentTransform> transition, [Composable] Action<T> content, bool animateSize = false, float transitionDuration = ComposeDefaults.TransitionDuration, IModifier? modifier = null)
    {
        if (CurrentComposer.BeginComposeGroup((value, transition, content, animateSize, transitionDuration, modifier)))
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
            var(containerModifier, contentModifier) = animateSize ? AnimateSizeModifiers(transitionDuration) : (Modifier, Modifier);
            // Layout:
            var resolvedTransition = Remember(value!, () => Equals(previousValue.Value, value) ? ContentTransform() : transition(previousValue.Value, value));
            ReusableComposeView<AnimatedContent>(modifier: modifier.OrEmpty().Then(containerModifier), content: RememberComposable<global::System.Action>((value, content, isSwitched, resolvedProgress, previousValue, contentModifier, resolvedTransition), () =>
            {
                var parent = LocalParentLayout.Current;
                var nextModifier = resolvedTransition.Enter.Get(resolvedProgress, parent).Then(contentModifier);
                var previousModifier = resolvedTransition.Exit.Get(resolvedProgress, parent).Float();
                var isAnimationRunning = resolvedProgress is> 0 and < 1;
                var next = (Value: value, Style: nextModifier, Progress: resolvedProgress, ContentState: isAnimationRunning ? ContentState.Idle : ContentState.Entering);
                var previous = (Value: previousValue.Value, Style: previousModifier, Progress: 1 - resolvedProgress, ContentState: ContentState.Exiting);
                var pair = isSwitched.Value ? (First: next, Second: previous) : (First: previous, Second: next);
                if (isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "First", content: RememberComposable<global::System.Action>((content, pair), () =>
                    {
                        CompositionLocalProvider(provides: IImmutableStableList.Create(LocalModifier.Provides(after: pair.First.Style), LocalTransitionProgress.Provides(pair.First.Progress), LocalContentState.Provides(pair.First.ContentState)), content: RememberComposable<global::System.Action>((content, pair), () => content(pair.First.Value)));
                    }));
                }

                if (!isSwitched.Value || isAnimationRunning)
                {
                    Key(key: "Second", content: RememberComposable<global::System.Action>((content, pair), () =>
                    {
                        CompositionLocalProvider(provides: IImmutableStableList.Create(LocalModifier.Provides(after: pair.Second.Style), LocalTransitionProgress.Provides(pair.Second.Progress), LocalContentState.Provides(pair.Second.ContentState)), content: RememberComposable<global::System.Action>((content, pair), () => content(pair.Second.Value)));
                    }));
                }
            }));
        }
        finally
        {
            CurrentComposer.EndComposeGroup(() => __AnimatedContent<T>(value, transition, content, animateSize, transitionDuration, modifier));
        }
    }
}