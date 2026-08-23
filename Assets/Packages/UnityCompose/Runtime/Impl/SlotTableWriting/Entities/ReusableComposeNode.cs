using System;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal abstract class ReusableComposeNode
{
    public abstract void ReInsert(int index);

    public abstract VisualElement? GetVisualElement();

    public static ReusableComposeNode<T> Get<T>() where T : VisualElement, new()
    {
        return ReusableComposeNode<T>.Get();
    }
}

internal class ReusableComposeNode<T> : ReusableComposeNode, IComposeDisposable where T : VisualElement, new()
{
    private static readonly ObjectPool<ReusableComposeNode<T>> Pool = new(
        factory: () => new ReusableComposeNode<T>()
    );

    public T VisualElement { get; set; } = new T
    {
        pickingMode = PickingMode.Ignore
    };

    private int _indexInParent = -1;
    private IModifier? _lastModifier;
    private Action<T>? _lastInitializer;
    private bool _isDisposed;

    private readonly IMutableStableList<IModifier> _lastModifiers =
        IMutableStableList.Create<IModifier>();

    private readonly IMutableStableList<IModifier> _newModifiers =
        IMutableStableList.Create<IModifier>();

    public override VisualElement GetVisualElement() => VisualElement;

    public static ReusableComposeNode<T> Get()
    {
        var instance = ComposeConstants.Pooling ? Pool.Get() : new ReusableComposeNode<T>();
        instance._isDisposed = false;
        return instance;
    }

    private ReusableComposeNode()
    {
    }

    public void Update(
        VisualElement parent,
        int indexInParent,
        IModifier? modifier,
        Action<T>? initializer
    )
    {
        // if (VisualElement == null)
        //     return;
        if (ComposeConstants.Logging && VisualElement.GetType() == typeof(VisualElement))
            Debug.Log(indexInParent);
        if (_indexInParent != indexInParent)
        {
            parent.FastReinsert(indexInParent, VisualElement);
            _indexInParent = indexInParent;
        }

        if (!Equals(_lastModifier, modifier))
        {
            _newModifiers.Clear();
            modifier?.Flatten(_newModifiers);
            foreach (var eachModifier in _lastModifiers)
            {
                if (_newModifiers.Contains(eachModifier))
                    continue;
                eachModifier.Revert(VisualElement);
            }

            foreach (var eachModifier in _newModifiers)
            {
                if (_lastModifiers.Contains(eachModifier))
                    continue;
                eachModifier.Apply(VisualElement);
            }

            _lastModifiers.Clear();
            foreach (var eachModifier in _newModifiers)
                _lastModifiers.Add(eachModifier);

            _newModifiers.Clear();
            _lastModifier = modifier;
        }

        if (initializer != null && _lastInitializer != initializer)
        {
            initializer(VisualElement);
            _lastInitializer = initializer;
        }
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;
        _isDisposed = true;
        var visualElement = VisualElement;
        var parent = visualElement.parent;
        parent?.FastRemove(_indexInParent, visualElement);
        VisualElement = new T()
        {
            pickingMode = PickingMode.Ignore
        };

        _indexInParent = -1;
        _lastModifier = null;
        _lastInitializer = null;
        _lastModifiers.Clear();
        _newModifiers.Clear();

        Pool.Return(this);
    }

    public override void ReInsert(int index)
    {
        if (_indexInParent == index)
            return;
        _indexInParent = index;
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