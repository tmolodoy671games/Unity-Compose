// ReSharper disable CheckNamespace

using System;

namespace UnityCompose;

public readonly record struct TransitionState(
    ContentState State,
    float Progress,
    float AbsoluteProgress,
    float TimeElapsed,
    float AbsoluteTimeElapsed,
    float Duration
)
{
    public static TransitionState Create(
        ContentState state,
        float absoluteProgress,
        float duration
    )
    {
        var progress = state switch
        {
            ContentState.Entering => absoluteProgress,
            ContentState.Idle => 1,
            ContentState.Exiting => 1 - absoluteProgress,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
        return new TransitionState(
            State: state,
            Progress: progress,
            AbsoluteProgress: absoluteProgress,
            TimeElapsed: progress * duration,
            AbsoluteTimeElapsed: absoluteProgress * duration,
            Duration: duration
        );
    }
}