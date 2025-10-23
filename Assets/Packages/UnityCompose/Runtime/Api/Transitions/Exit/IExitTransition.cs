// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IExitTransition EmptyExit => EmptyExitTransitionImpl.Instance;
}

public interface IExitTransition
{
    IModifier Get(IBoxScope scope, float progress, LayoutInfo parent);

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

    public IModifier Get(IBoxScope scope, float progress, LayoutInfo parent)
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

    public IModifier Get(IBoxScope scope, float progress, LayoutInfo parent)
    {
        return _left.Get(scope, progress, parent)
            .Then(_right.Get(scope, progress, parent));
    }
}