using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal abstract class ReusableComposeNode
{
    public abstract void ReInsert(int index);
    
    public static ReusableComposeNode<T> Get<T>() where T : VisualElement
    {
        return ReusableComposeNode<T>.Get();
    }
}

internal class ReusableComposeNode<T> : ReusableComposeNode, IDisposable where T : VisualElement
{
    private static readonly NewObjectPool<ReusableComposeNode<T>> _pool = new NewObjectPool<ReusableComposeNode<T>>(
        factory: () => new ReusableComposeNode<T>()
    );

    public T? VisualElement;
    private int _indexInParent = -1;
    private IModifier? _lastModifier;
    private Action<T>? _lastInitializer;

    private readonly IMutableStableSet<ComposeModifiedProperty> _lastProperties =
        IMutableStableSet.Create<ComposeModifiedProperty>();

    private readonly IMutableStableSet<ComposeModifiedProperty> _newProperties =
        IMutableStableSet.Create<ComposeModifiedProperty>();

    public static ReusableComposeNode<T> Get()
    {
        return _pool.Get();
    }
    
    private ReusableComposeNode() {}

    public void Update(
        VisualElement parent,
        int indexInParent,
        IModifier? modifier,
        Action<T>? initializer
    )
    {
        if (VisualElement == null)
            return;
        if (_indexInParent != indexInParent)
        {
            parent.FastReinsert(indexInParent, VisualElement);
            _indexInParent = indexInParent;
        }

        if (!Equals(_lastModifier, modifier))
        {
            modifier?.Apply(_newProperties);
            foreach (var property in _lastProperties)
            {
                if (_newProperties.Contains(property))
                    continue;
                property.Revert(VisualElement);
            }

            _lastProperties.Clear();
            if (_newProperties.IsNotEmpty())
                _lastProperties.AddRange(_newProperties);
            _newProperties.Clear();

            VisualElement.ClearCallbacks();
            VisualElement.style.transitionDelay.value?.Clear();
            VisualElement.style.transitionDuration.value?.Clear();
            VisualElement.style.transitionProperty.value?.Clear();
            VisualElement.style.transitionTimingFunction.value?.Clear();
            VisualElement.pickingMode = PickingMode.Ignore;
            VisualElement.style.overflow = Overflow.Visible;
            _lastModifier = modifier;
            modifier?.Apply(VisualElement);
        }

        if (initializer != null && _lastInitializer != initializer)
        {
            initializer(VisualElement);
            _lastInitializer = initializer;
        }
    }

    public void Dispose()
    {
        var visualElement = VisualElement;
        if (visualElement != null)
        {
            var parent = visualElement.parent;
            parent?.FastRemove(_indexInParent, visualElement);
        }

        VisualElement = null;
        _indexInParent = -1;
        _lastModifier = null;
        _lastInitializer = null;
        
        _pool.Return(this);
    }

    public override void ReInsert(int index)
    {
        if (_indexInParent == index)
            return;
        _indexInParent = index;
        if (VisualElement == null)
            return;
        var parent = VisualElement.parent;
        if (parent == null)
            return;
        if (parent.GetOrNull(_indexInParent) == VisualElement)
            return;
        parent.FastRemove(_indexInParent, VisualElement);
        parent.FastReinsert(index, VisualElement);
    }

    public override string ToString()
    {
        return $"ReusableComposeNode({VisualElement?.Format()})";
    }
}