// ReSharper disable CheckNamespace

using System;

namespace UnityCompose;

public readonly record struct TransitionState(
    TransitionPhase Phase,
    float Progress,
    float AbsoluteProgress,
    float TimeElapsed,
    float AbsoluteTimeElapsed,
    float Duration
)
{
    public static TransitionState Create(
        TransitionPhase phase,
        float absoluteProgress,
        float duration
    )
    {
        var progress = phase switch
        {
            TransitionPhase.Entering => absoluteProgress,
            TransitionPhase.Idle => 1,
            TransitionPhase.Exiting => 1 - absoluteProgress,
            _ => throw new ArgumentOutOfRangeException(nameof(phase), phase, null)
        };
        return new TransitionState(
            Phase: phase,
            Progress: progress,
            AbsoluteProgress: absoluteProgress,
            TimeElapsed: progress * duration,
            AbsoluteTimeElapsed: absoluteProgress * duration,
            Duration: duration
        );
    }

    public static TransitionState Create(
        TransitionState state,
        TransitionState parent
    )
    {
        var resolvedPhase = state.Phase < parent.Phase ? state.Phase : parent.Phase;
        var resolvedAbsoluteProgress = Math.Min(state.AbsoluteProgress, parent.AbsoluteProgress);
        var resolvedDuration = Math.Max(state.Duration, parent.Duration);
        return Create(
            phase: resolvedPhase,
            absoluteProgress: resolvedAbsoluteProgress,
            duration: resolvedDuration
        );
    }
}