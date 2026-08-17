using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public partial interface IModifier
{
    void Apply(VisualElement element);
    void Revert(VisualElement element);
    void Flatten(IMutableStableCollection<IModifier> modifiers);
    bool IsComposable => false;
    IModifier Compose() => this;

    public static IModifier operator +(IModifier left, IModifier right)
    {
        return left.Then(right);
    }
}

public abstract class BaseModifier<T> : IModifier where T : BaseModifier<T>
{
    public abstract void Apply(VisualElement element);

    public abstract void Revert(VisualElement element);

    public virtual void Flatten(IMutableStableCollection<IModifier> modifiers)
    {
        modifiers.Add(this);
    }

    protected abstract bool Equals(T other);
    
    public virtual bool IsComposable => false;
    public virtual IModifier Compose() => this;

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

public static partial class ModifierExtensions
{
    public static IModifier OrEmpty(this IModifier? modifier) => modifier ?? Modifier;

    public static IModifier Then(this IModifier? left, IModifier? right)
    {
        if (left == null || Equals(left, EmptyModifierImpl.Instance))
            return right.OrEmpty();
        if (right == null || Equals(right, EmptyModifierImpl.Instance))
            return left.OrEmpty();
        return new CompositeModifierImpl(left, right);
    }
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

    public override void Flatten(IMutableStableCollection<IModifier> modifiers)
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
        Composable = _first.IsComposable || _second.IsComposable;
    }

    public override void Apply(VisualElement element)
    {
        _first.Apply(element);
        _second.Apply(element);
    }

    public override void Flatten(IMutableStableCollection<IModifier> modifiers)
    {
        _first.Flatten(modifiers);
        _second.Flatten(modifiers);
    }

    public bool Composable { get; }

    public override IModifier Compose()
    {
        var firstComposed = _first.Compose();
        var secondComposed = _second.Compose();
        if (firstComposed != _first || _second != secondComposed)
            return new CompositeModifierImpl(firstComposed, secondComposed);
        return this;
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

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append(_first);
        builder.AppendLine();
        builder.Append(_second);
        return builder.ToString();
    }
}