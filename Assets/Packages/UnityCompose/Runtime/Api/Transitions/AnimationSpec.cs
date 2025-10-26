// ReSharper disable CheckNamespace

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
            easing: easing ?? EaseInOut
        );
    }
}

public readonly record struct AnimationSpec
{
    private readonly float _duration;
    private readonly float _delay;
    private readonly IEasing _easing;
    public readonly bool HasValue;

    internal AnimationSpec(float duration, float delay, IEasing easing) : this()
    {
        _duration = duration;
        _delay = delay;
        _easing = easing;
        HasValue = true;
    }

    public float GetProgress(float timeElapsed)
    {
        if (timeElapsed < _delay)
            return 0f;

        if (timeElapsed >= _delay + _duration)
            return 1f;

        return _easing.Transform((timeElapsed - _delay) / _duration);
    }

    public float TotalDuration() => _delay + _duration;

    public static AnimationSpec Default = Tween(
        duration: ComposeDefaults.TransitionDuration,
        delay: 0f,
        easing: EaseInOut
    );
}