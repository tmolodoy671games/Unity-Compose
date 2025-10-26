// ReSharper disable CheckNamespace

using UnityEngine;

namespace UnityCompose;

public interface IExitTransition
{
    float TotalDuration { get; }
    
    IModifier Get(float timeElapsed, LayoutInfo parent);

    public static IExitTransition Empty => EmptyExitTransitionImpl.Instance;

    public static IExitTransition operator +(IExitTransition first, IExitTransition second)
    {
        return new CompositeExitTransitionImpl(first, second);
    }
}

internal class EmptyExitTransitionImpl : IExitTransition
{
    public static readonly EmptyExitTransitionImpl Instance = new();

    private EmptyExitTransitionImpl()
    {
    }

    public float TotalDuration => 0f;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        return Modifier;
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
}