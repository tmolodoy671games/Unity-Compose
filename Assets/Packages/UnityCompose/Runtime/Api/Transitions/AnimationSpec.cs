// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityEngine;

namespace UnityCompose;

public partial class ComposeFunctions
{
    public static AnimationSpec Tween(
        float duration = ComposeDefaults.TransitionDuration,
        float delay = 0f,
        IEasing? easing = null
    )
    {
        return new AnimationSpec(
            duration: duration,
            delay: delay,
            easing: easing ?? EaseInOutEasing
        );
    }
}

public readonly record struct AnimationSpec
{
    public readonly float Duration;
    public readonly float Delay;
    private readonly IEasing _easing;

    internal AnimationSpec(float duration, float delay, IEasing easing) : this()
    {
        Duration = duration;
        Delay = delay;
        _easing = easing;
    }

    public float GetProgress(float timeElapsed)
    {
        var progress = Mathf.Clamp01((timeElapsed - Delay) / Duration);
        return _easing.Transform(progress);
    }

    public float TotalDuration => Delay + Duration;

    public AnimationSpec With(
        float duration = -1,
        float delay = -1,
        IEasing? easing = null
    )
    {
        return new AnimationSpec(
            duration: duration >= 0 ? duration : Duration,
            delay: delay >= 0 ? delay : Delay,
            easing: easing ?? _easing
        );
    }

    public override string ToString()
    {
        return $"AnimationSpec(Delay: {Delay}, Duration: {Duration}, Easing: {_easing})";
    }

    public static AnimationSpec Default = Tween(
        delay: 0f,
        duration: ComposeDefaults.TransitionDuration,
        easing: EaseInOutEasing
    );
}

public static class OptionalAnimationSpecExtensions
{
    public static AnimationSpec GetOrDefault(this Optional<AnimationSpec> animationSpec)
    {
        return animationSpec.HasValue ? animationSpec.Value : AnimationSpec.Default;
    }
}