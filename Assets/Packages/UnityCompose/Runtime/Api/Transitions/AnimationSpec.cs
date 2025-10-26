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
            Duration: duration,
            Delay: delay,
            Easing: easing ?? EaseInOut,
            HasValue: true
        );
    }
}

public readonly record struct AnimationSpec(
    float Duration,
    float Delay,
    IEasing Easing,
    bool HasValue
);