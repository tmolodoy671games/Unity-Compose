using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static ExitTransition SlideOut(SlideDirection direction) =>
        new ExitTransition.SlideOut(direction);

    public static ExitTransition FadeOut() => new ExitTransition.FadeOut();

    public static ExitTransition Exit(Func<float, IResolvedStyle, ComposeStyle> factory) =>
        new ExitTransition.Custom(factory);

    public static EnterTransition SlideIn(SlideDirection direction) =>
        new EnterTransition.SlideIn(direction);

    public static EnterTransition FadeIn() => new EnterTransition.FadeIn();
    
    public static EnterTransition Enter(Func<float, IResolvedStyle, ComposeStyle> factory) =>
        new EnterTransition.Custom(factory);

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