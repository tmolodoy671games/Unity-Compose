// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public interface IExitTransition
{
    float TotalDuration { get; }

    IModifier Get(float timeElapsed, LayoutInfo parent);

    IExitTransition With(AnimationSpec animationSpec);

    public static IExitTransition Empty(AnimationSpec animationSpec = default) =>
        new EmptyExitTransitionImpl(animationSpec);

    public static IExitTransition operator +(IExitTransition first, IExitTransition second)
    {
        return new CompositeExitTransitionImpl(first, second);
    }
}

internal class EmptyExitTransitionImpl : IExitTransition
{
    private readonly AnimationSpec _animationSpec;

    public EmptyExitTransitionImpl(AnimationSpec animationSpec)
    {
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => _animationSpec.TotalDuration;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        return Modifier;
    }

    public IExitTransition With(AnimationSpec animationSpec)
    {
        return new EmptyExitTransitionImpl(animationSpec);
    }
}

internal class CompositeExitTransitionImpl : IExitTransition
{
    private readonly IExitTransition _left;
    private readonly IExitTransition _right;

    public CompositeExitTransitionImpl(IExitTransition left, IExitTransition right)
    {
        _left = left;
        _right = right;
    }

    public float TotalDuration => Mathf.Max(_left.TotalDuration, _right.TotalDuration);

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        return _left.Get(timeElapsed, parent)
            .Then(_right.Get(timeElapsed, parent));
    }

    public IExitTransition With(AnimationSpec animationSpec)
    {
        return new CompositeExitTransitionImpl(
            left: _left.With(animationSpec),
            right: _right.With(animationSpec)
        );
    }
}