using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Views;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static readonly ICompositionLocal<float> LocalTransitionProgress = CompositionLocalOf(() => 1f);

    public static ContentTransform InstantContentTransform => UnityCompose.ContentTransform.Instant;

    public static ContentTransform ContentTransform(
        IEnterTransition? enter = null,
        IExitTransition? exit = null
    ) => new(enter ?? EmptyEnterTransitionImpl.Instance, exit ?? EmptyExitTransitionImpl.Instance);

    [Composable]
    public static void AnimatedContent<T>(
        T value,
        Func<T, T, ContentTransform> transition,
        [Composable] Action<T> content,
        bool animateSize = false,
        float transitionDuration = ComposeDefaults.TransitionDuration,
        IModifier? modifier = null
    )
    {
        // Progress:
        var isSwitched = Remember(() => MutableStateOf(false));
        LaunchedEffect(value!, () => isSwitched.Value = !isSwitched.Value);

        var progress = AnimateFloatAsState(isSwitched.Value ? 1 : 0f, transitionDuration).Value;
        var resolvedProgress = isSwitched.Value ? progress : 1 - progress;

        var previousValue = Remember(() => IMutableStableProperty.Create(value));
        var targetValue = Remember(() => IMutableStableProperty.Create(value));
        LaunchedEffect(value!, () =>
        {
            previousValue.Value = targetValue.Value;
            targetValue.Value = value;
        });

        // Animating size:
        var (containerModifier, contentModifier) = animateSize
            ? AnimateSizeModifiers(transitionDuration)
            : (Modifier, Modifier);

        // Layout:
        var resolvedTransition = Remember(
            value!,
            () => Equals(previousValue.Value, value) ? ContentTransform() : transition(previousValue.Value, value)
        );

        ReusableComposeView<AnimatedContent>(
            modifier: modifier.OrEmpty()
                .Then(containerModifier),
            content: () =>
            {
                var parent = LocalParentLayout.Current;
                var nextModifier = resolvedTransition.Enter.Get(resolvedProgress, parent)
                    .Then(contentModifier);
                var previousModifier = resolvedTransition.Exit.Get(resolvedProgress, parent)
                    .Float();
                var isAnimationRunning = resolvedProgress is > 0 and < 1;

                var next = (Value: value, Style: nextModifier, Progress: resolvedProgress);
                var previous = (Value: previousValue.Value, Style: previousModifier, Progress: 1 - resolvedProgress);
                var pair = isSwitched.Value
                    ? (First: next, Second: previous)
                    : (First: previous, Second: next);

                if (isSwitched.Value || isAnimationRunning)
                {
                    Key(
                        key: "First",
                        content: () =>
                        {
                            CompositionLocalProvider(
                                provides: IImmutableStableList.Create(
                                    LocalModifier.Provides(after: pair.First.Style),
                                    LocalTransitionProgress.Provides(pair.First.Progress)
                                ),
                                content: () => content(pair.First.Value)
                            );
                        }
                    );
                }

                if (!isSwitched.Value || isAnimationRunning)
                {
                    Key(
                        key: "Second",
                        content: () =>
                        {
                            CompositionLocalProvider(
                                provides: IImmutableStableList.Create(
                                    LocalModifier.Provides(after: pair.Second.Style),
                                    LocalTransitionProgress.Provides(pair.Second.Progress)
                                ),
                                content: () => content(pair.Second.Value)
                            );
                        }
                    );
                }
            }
        );
    }
}