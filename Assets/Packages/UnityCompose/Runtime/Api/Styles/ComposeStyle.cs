using System.Diagnostics.CodeAnalysis;
using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public abstract class ComposeStyle
{
    private class EmptyComposeStyleImpl : ComposeStyle<EmptyComposeStyleImpl>
    {
        public static readonly EmptyComposeStyleImpl Instance = new();

        private EmptyComposeStyleImpl()
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

        protected override bool Compare(EmptyComposeStyleImpl other)
        {
            return true;
        }
    }

    private class CompositeComposeStyleImpl : ComposeStyle<CompositeComposeStyleImpl>
    {
        private readonly ComposeStyle _first;
        private readonly ComposeStyle _second;
        private readonly int _depth;

        public CompositeComposeStyleImpl(ComposeStyle first, ComposeStyle second)
        {
            _first = first;
            _second = second;
            _depth = (first is CompositeComposeStyleImpl firstComposite ? firstComposite._depth : 1) +
                     (second is CompositeComposeStyleImpl secondComposite ? secondComposite._depth : 1);
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

        protected override bool Compare(CompositeComposeStyleImpl other)
        {
            return _depth == other._depth &&
                   _first.Compare(other._first) &&
                   _second.Compare(other._second);
        }
    }

    public static ComposeStyle Empty => EmptyComposeStyleImpl.Instance;

    [Composable]
    public abstract void Apply(VisualElement element);
    [Composable]
    public abstract void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties);
    public abstract void Revert(VisualElement element);

    protected abstract bool Compare(ComposeStyle other);

    public ComposeStyle Then(ComposeStyle? composeStyle)
    {
        if (composeStyle == null)
            return this;
        if (Equals(this, Empty))
            return composeStyle;
        if (Equals(composeStyle, Empty))
            return this;
        return new CompositeComposeStyleImpl(this, composeStyle);
    }

    public static ComposeStyle operator +(ComposeStyle style1, ComposeStyle style2)
    {
        return style1.Then(style2);
    }

    public override bool Equals(object? obj)
    {
        if (obj == null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Compare((ComposeStyle)obj);
    }

    [SuppressMessage("ReSharper", "BaseObjectGetHashCodeCallInGetHashCode")]
    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}

public abstract class ComposeStyle<T> : ComposeStyle where T : ComposeStyle<T>
{
    protected sealed override bool Compare(ComposeStyle other)
    {
        return other is T otherStyle && Compare(otherStyle);
    }

    protected abstract bool Compare(T other);
}

public static partial class ComposeStyleExtensions
{
    public static ComposeStyle OrEmpty(this ComposeStyle? style) => style ?? ComposeStyle.Empty;
}