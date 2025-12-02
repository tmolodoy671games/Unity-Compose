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
    private readonly float _duration;
    private readonly float _delay;
    private readonly IEasing _easing;

    internal AnimationSpec(float duration, float delay, IEasing easing) : this()
    {
        _duration = duration;
        _delay = delay;
        _easing = easing;
    }

    public float GetProgress(float timeElapsed)
    {
        var progress = Mathf.Clamp01((timeElapsed - _delay) / _duration);
        return _easing.Transform(progress);
    }

    public float Delay => _delay;
    public float TotalDuration => _delay + _duration;

    public AnimationSpec With(
        float duration = -1,
        float delay = -1,
        IEasing? easing = null
    )
    {
        return new AnimationSpec(
            duration: duration >= 0 ? duration : _duration,
            delay: delay >= 0 ? delay : _delay,
            easing: easing ?? _easing
        );
    }

    public override string ToString()
    {
        return $"AnimationSpec(Delay: {_delay}, Duration: {_duration}, Easing: {_easing})";
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