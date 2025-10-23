using SharpExtensions;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public readonly record struct ComposeTransition(
    float Duration = ComposeDefaults.TransitionDuration,
    float Delay = 0f,
    EasingMode TimingFunction = EasingMode.EaseInOut
)
{
    public static readonly ComposeTransition Default = new(
        Duration: ComposeDefaults.TransitionDuration,
        Delay: 0f,
        TimingFunction: EasingMode.EaseInOut
    );

    public bool IsDefault() => Duration.AlmostEquals(0f);
    public bool HasValue => Duration > 0;
}