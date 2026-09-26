using System;
using Compose.Net;
using SharpExtensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Exit;

internal class ExitTransitionsFactoryImpl : IExitTransitionsFactory
{
    public IExitTransition FadeOut(float targetAlpha, Optional<AnimationSpec> animationSpec)
    {
        return new FadeOutExitTransitionImpl(targetAlpha, animationSpec.GetOrDefault());
    }

    public IExitTransition ScaleOut(float targetScale, Optional<AnimationSpec> animationSpec)
    {
        return new ScaleOutExitTransitionImpl(targetScale, animationSpec.GetOrDefault());
    }

    public IExitTransition SlideOut(Func<Offset, Offset> targetOffset, Optional<AnimationSpec> animationSpec)
    {
        return new SlideOutExitTransitionImpl(targetOffset, animationSpec.GetOrDefault());
    }

    public IExitTransition SlideOutHorizontally(
        Func<float, float> targetOffsetX,
        Optional<AnimationSpec> animationSpec
    )
    {
        return new SlideOutHorizontalTransitionImpl(targetOffsetX, animationSpec.GetOrDefault());
    }

    public IExitTransition SlideOutVertically(Func<float, float> targetOffsetY, Optional<AnimationSpec> animationSpec)
    {
        return new SlideOutVerticalTransitionImpl(targetOffsetY, animationSpec.GetOrDefault());
    }
}