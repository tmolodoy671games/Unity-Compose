// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IEnterTransition EmptyEnter => EmptyEnterTransitionImpl.Instance;
}

public interface IEnterTransition
{
    IModifier Get(float progress, LayoutInfo parent);

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

    public IModifier Get(float progress, LayoutInfo parent)
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

    public IModifier Get(float progress, LayoutInfo parent)
    {
        return _left.Get(progress, parent)
            .Then(_right.Get(progress, parent));
    }
}