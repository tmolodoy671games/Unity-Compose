using System;
using System.Text;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal interface IComposeGroupState : IDisposable
{
    internal abstract class Reusable : IComposeGroupState
    {
        public object? ObjectKey;
        public readonly ComposeGroupRestartScope RestartScope;
        public CompositionLocalMap? CompositionLocalMap;
        public VisualElement? Element;
        public int ElementIndex;
        public int ElementsCount;

        protected Reusable(SlotWriter writer)
        {
            RestartScope = new ComposeGroupRestartScope(writer);
        }

        public void Dispose()
        {
        }
    }

    internal class Reusable<T> : Reusable
    {
        public Optional<T> PreviousState;

        public Reusable(SlotWriter writer, T initialState) : base(writer)
        {
            PreviousState = initialState;
        }

        public override string ToString()
        {
            var builder = new StringBuilder("ComposeGroupData(");
            builder.Append($"ObjectKey = {ObjectKey}, ");
            builder.Append($"PreviousState = {PreviousState}");
            builder.Append($"RestartScope = {RestartScope.Restart != null}");
            builder.Append($"CompositionLocalMap = {CompositionLocalMap}, ");
            builder.Append($"Element = {Element?.Format()}");
            builder.Append(")");
            return builder.ToString();
        }
    }
    
    internal abstract class Replaceable : IComposeGroupState
    {
        public abstract void Dispose();
    }
    
    internal class Replaceable<TKey, TValue> : Replaceable
    {
        public Optional<TKey> Key { get; set; }
        public TValue Value { get; set; } = default!;

        public override string ToString()
        {
            return $"({Key}: {Value})";
        }

        public override void Dispose()
        {
            if (Value != null && Value is IDisposable disposable)
                disposable.Dispose();
        }
    }
}