using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal abstract class ReusableComposeNode
{
    public abstract void ReInsert(int index);

    public abstract VisualElement? GetVisualElement();

    public static ReusableComposeNode<T> Get<T>() where T : VisualElement
    {
        return ReusableComposeNode<T>.Get();
    }
}

internal class ReusableComposeNode<T> : ReusableComposeNode, IDisposable where T : VisualElement
{
    private static readonly NewObjectPool<ReusableComposeNode<T>> _pool = new(
        factory: () => new ReusableComposeNode<T>()
    );

    public T? VisualElement;
    private int _indexInParent = -1;
    private IModifier? _lastModifier;
    private Action<T>? _lastInitializer;
    private bool _isDisposed;

    private readonly IMutableStableList<IModifier> _lastModifiers =
        IMutableStableList.Create<IModifier>();

    private readonly IMutableStableList<IModifier> _newModifiers =
        IMutableStableList.Create<IModifier>();

    public override VisualElement? GetVisualElement() => VisualElement;

    public static ReusableComposeNode<T> Get()
    {
        var instance = _pool.Get();
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
        if (VisualElement == null)
            return;
        if (_indexInParent != indexInParent)
        {
            parent.FastReinsert(indexInParent, VisualElement);
            _indexInParent = indexInParent;
        }

        if (!Equals(_lastModifier, modifier))
        {
            _newModifiers.Clear();
            modifier?.Flatten(_newModifiers);
            for (var i = 0; i < _lastModifiers.Count; i++)
            {
                var eachModifier = _lastModifiers[i];
                if (_newModifiers.Contains(eachModifier))
                    continue;
                eachModifier.Revert(VisualElement);
            }

            _lastModifiers.Clear();
            if (_newModifiers.IsNotEmpty())
            {
                for (var i = 0; i < _newModifiers.Count; i++)
                    _lastModifiers.Add(_newModifiers[i]);
            }

            _newModifiers.Clear();
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
        if (_isDisposed)
            return;
        _isDisposed = true;
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