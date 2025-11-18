// ReSharper disable CheckNamespace

using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IEnterTransition
{
    float TotalDuration { get; }

    IModifier Get(float timeElapsed, VisualElement parent);

    IEnterTransition With(AnimationSpec animationSpec);

    public static IEnterTransition Empty(Optional<AnimationSpec> animationSpec = default) =>
        new EmptyEnterTransitionImpl(animationSpec.GetOrDefault());

    public static IEnterTransition operator +(IEnterTransition first, IEnterTransition second)
    {
        return new CompositeEnterTransitionImpl(first, second);
    }
}

internal class EmptyEnterTransitionImpl : IEnterTransition
{
    private readonly AnimationSpec _animationSpec;

    public EmptyEnterTransitionImpl(AnimationSpec animationSpec)
    {
        _animationSpec = animationSpec;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, VisualElement parent)
    {
        if (timeElapsed < _animationSpec.Delay)
            return Modifier.Alpha(0f);
        return Modifier;
    }

    public IEnterTransition With(AnimationSpec animationSpec)
    {
        return new EmptyEnterTransitionImpl(animationSpec: animationSpec);
    }
}

internal class CompositeEnterTransitionImpl : IEnterTransition
{
    private readonly IEnterTransition _left;
    private readonly IEnterTransition _right;

    public CompositeEnterTransitionImpl(IEnterTransition left, IEnterTransition right)
    {
        _left = left;
        _right = right;
    }

    public float TotalDuration => Mathf.Max(_left.TotalDuration, _right.TotalDuration);

    public IModifier Get(float timeElapsed, VisualElement parent)
    {
        return _left.Get(timeElapsed, parent)
            .Then(_right.Get(timeElapsed, parent));
    }

    public IEnterTransition With(AnimationSpec animationSpec)
    {
        return new CompositeEnterTransitionImpl(
            left: _left.With(animationSpec),
            right: _right.With(animationSpec)
        );
    }
}