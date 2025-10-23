using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public abstract class IModifier
{
    private class EmptyModifierImpl : IModifier<EmptyModifierImpl>
    {
        public static readonly EmptyModifierImpl Instance = new();

        private EmptyModifierImpl()
        {
        }

        public override void Apply(VisualElement element)
        {
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(EmptyModifierImpl other)
        {
            return true;
        }
    }

    private class CompositeModifierImpl : IModifier<CompositeModifierImpl>
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

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            _first.Apply(modifiedProperties);
            _second.Apply(modifiedProperties);
        }

        public override void Revert(VisualElement element)
        {
            _first.Revert(element);
            _second.Revert(element);
        }

        protected override bool Compare(CompositeModifierImpl other)
        {
            return _depth == other._depth &&
                   _first.Compare(other._first) &&
                   _second.Compare(other._second);
        }
    }

    public static IModifier Empty => EmptyModifierImpl.Instance;

    [Composable]
    public abstract void Apply(VisualElement element);
    [Composable]
    public abstract void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties);
    public abstract void Revert(VisualElement element);

    protected abstract bool Compare(IModifier other);

    public IModifier Then(IModifier? composeStyle)
    {
        if (composeStyle == null)
            return this;
        if (Equals(this, Empty))
            return composeStyle;
        if (Equals(composeStyle, Empty))
            return this;
        return new CompositeModifierImpl(this, composeStyle);
    }

    public static IModifier operator +(IModifier style1, IModifier style2)
    {
        return style1.Then(style2);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Compare((IModifier)obj);
    }

    [SuppressMessage("ReSharper", "BaseObjectGetHashCodeCallInGetHashCode")]
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public abstract class IModifier<T> : IModifier where T : IModifier<T>
{
    protected sealed override bool Compare(IModifier other)
    {
        return other is T otherStyle && Compare(otherStyle);
    }

    protected abstract bool Compare(T other);
}

public static partial class ComposeStyleExtensions
{
    public static IModifier OrEmpty(this IModifier? style) => style ?? IModifier.Empty;
}