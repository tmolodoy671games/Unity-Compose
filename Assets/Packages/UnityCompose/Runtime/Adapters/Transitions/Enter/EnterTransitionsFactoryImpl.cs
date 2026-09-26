using System;
using Compose.Net;
using SharpExtensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Transitions.Enter;

internal class EnterTransitionsFactoryImpl : IEnterTransitionsFactory
{
    public IEnterTransition FadeIn(float initialAlpha, Optional<AnimationSpec> animationSpec)
    {
        return new FadeInEnterTransitionImpl(initialAlpha, animationSpec.GetOrDefault());
    }

    public IEnterTransition ScaleIn(float initialScale, Optional<AnimationSpec> animationSpec)
    {
        return new ScaleInEnterTransitionImpl(initialScale, animationSpec.GetOrDefault());
    }

    public IEnterTransition SlideIn(Func<Offset, Offset> initialOffset, Optional<AnimationSpec> animationSpec)
    {
        return new SlideInEnterTransitionImpl(initialOffset, animationSpec.GetOrDefault());
    }

    public IEnterTransition SlideInHorizontally(
        Func<float, float> initialOffsetX,
        Optional<AnimationSpec> animationSpec
    )
    {
        return new SlideInHorizontallyTransitionImpl(initialOffsetX, animationSpec.GetOrDefault());
    }

    public IEnterTransition SlideInVertically(
        Func<float, float> initialOffsetY,
        Optional<AnimationSpec> animationSpec
    )
    {
        return new SlideInVerticallyTransitionImpl(initialOffsetY, animationSpec.GetOrDefault());
    }
}