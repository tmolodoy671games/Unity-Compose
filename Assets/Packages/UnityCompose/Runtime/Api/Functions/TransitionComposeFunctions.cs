using System.Diagnostics.CodeAnalysis;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static ComposeTransition Transition() => ComposeTransition.Default;

    [SuppressMessage("ReSharper", "MethodOverloadWithOptionalParameter")]
    public static ComposeTransition Transition(
        float duration = ComposeDefaults.TransitionDuration,
        float delay = 0f,
        EasingMode easingMode = EasingMode.EaseInOut
    )
    {
        return new ComposeTransition(duration, delay, easingMode);
    }
}