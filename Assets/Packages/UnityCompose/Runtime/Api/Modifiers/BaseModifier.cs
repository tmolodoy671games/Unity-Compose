using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityEngine.UIElements;
using static UnityCompose.ComposeFunctions;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public interface IModifier
{
    [Composable]
    void Apply(VisualElement element);

    [Composable]
    void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties);

    [Composable]
    void Revert(VisualElement element);

    public IModifier Then(IModifier? composeStyle)
    {
        if (composeStyle == null)
            return this;
        if (Equals(this, Modifier))
            return composeStyle;
        if (Equals(composeStyle, Modifier))
            return this;
        return new CompositeModifierImpl(this, composeStyle);
    }

    public static IModifier operator +(IModifier style1, IModifier style2)
    {
        return style1.Then(style2);
    }
}

public abstract class BaseModifier<T> : IModifier where T : BaseModifier<T>
{
    public abstract void Apply(VisualElement element);

    public abstract void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties);

    public abstract void Revert(VisualElement element);
    
    protected abstract bool Equals(T other);

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((T)obj);
    }

    [SuppressMessage("ReSharper", "BaseObjectGetHashCodeCallInGetHashCode")]
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public static partial class ComposeStyleExtensions
{
    public static IModifier OrEmpty(this IModifier? style) => style ?? Modifier;
}

internal class EmptyModifierImpl : BaseModifier<EmptyModifierImpl>
{
    public static readonly EmptyModifierImpl Instance = new();

    private EmptyModifierImpl()
    {
    }

    public override void Apply(VisualElement element)
    {
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
    }

    public override void Revert(VisualElement element)
    {
    }

    protected override bool Equals(EmptyModifierImpl other)
    {
        return true;
    }
}

internal class CompositeModifierImpl : BaseModifier<CompositeModifierImpl>
{
    private readonly IModifier _first;
    private readonly IModifier _second;
    private readonly int _depth;

    public CompositeModifierImpl(IModifier first, IModifier second)
    {
        _first = first;
        _second = second;
        _depth = (first is CompositeModifierImpl firstComposite ? firstComposite._depth : 1) +
                 (second is CompositeModifierImpl secondComposite ? secondComposite._depth : 1);
    }

    public override void Apply(VisualElement element)
    {
        _first.Apply(element);
        _second.Apply(element);
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        _first.Apply(modifiedProperties);
        _second.Apply(modifiedProperties);
    }

    public override void Revert(VisualElement element)
    {
        _first.Revert(element);
        _second.Revert(element);
    }

    protected override bool Equals(CompositeModifierImpl other)
    {
        return _depth == other._depth &&
               _first.Equals(other._first) &&
               _second.Equals(other._second);
    }
}