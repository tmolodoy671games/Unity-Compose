// ReSharper disable CheckNamespace

namespace UnityCompose;

public interface IExitTransition
{
    IModifier Get(float progress, LayoutInfo parent);

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

    public IModifier Get(float progress, LayoutInfo parent)
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

    public IModifier Get(float progress, LayoutInfo parent)
    {
        return _left.Get( progress, parent)
            .Then(_right.Get( progress, parent));
    }
}