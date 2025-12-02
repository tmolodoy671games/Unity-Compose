// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition Hide(Optional<AnimationSpec> animationSpec = default) =>
        new HideExitTransitionImpl(animationSpec.GetOrDefault());
}

internal class HideExitTransitionImpl : IExitTransition
{
    private readonly AnimationSpec _animationSpec;

    public HideExitTransitionImpl(AnimationSpec animationSpec)
    {
        _animationSpec = animationSpec;
    }

    public float TotalDuration => 0f;

    public IModifier Get(float timeElapsed, VisualElement parent)
    {
        if (timeElapsed < _animationSpec.Delay)
            return Modifier;
        return Modifier
            .Alpha(0f);
    }

    public IExitTransition With(AnimationSpec animationSpec)
    {
        return new HideExitTransitionImpl(animationSpec);
    }
}