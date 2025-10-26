// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public interface IEnterTransition
{
    float TotalDuration { get; }
    
    IModifier Get(float timeElapsed, LayoutInfo parent);

    public static IEnterTransition Empty => EmptyEnterTransitionImpl.Instance;

    public static IEnterTransition operator +(IEnterTransition first, IEnterTransition second)
    {
        return new CompositeEnterTransitionImpl(first, second);
    }
}

internal class EmptyEnterTransitionImpl : IEnterTransition
{
    public static readonly EmptyEnterTransitionImpl Instance = new();

    private EmptyEnterTransitionImpl()
    {
    }

    public float TotalDuration => 0f;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        return Modifier;
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

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        return _left.Get(timeElapsed, parent)
            .Then(_right.Get(timeElapsed, parent));
    }
}