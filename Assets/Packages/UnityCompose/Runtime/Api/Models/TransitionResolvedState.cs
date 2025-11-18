// ReSharper disable CheckNamespace

using System;

namespace UnityCompose;

internal readonly record struct TransitionResolvedState(
    TransitionState State,
    float Progress,
    float AbsoluteProgress,
    float TimeElapsed,
    float AbsoluteTimeElapsed,
    float Duration
)
{
    public static TransitionResolvedState Create(
        TransitionState state,
        float absoluteProgress,
        float duration
    )
    {
        var progress = state switch
        {
            TransitionState.Entering => absoluteProgress,
            TransitionState.Idle => 1,
            TransitionState.Exiting => 1 - absoluteProgress,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
        return new TransitionResolvedState(
            State: state,
            Progress: progress,
            AbsoluteProgress: absoluteProgress,
            TimeElapsed: progress * duration,
            AbsoluteTimeElapsed: absoluteProgress * duration,
            Duration: duration
        );
    }
}