using System;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Extensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Writer;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

internal static class ReusableComposeNode
{
    public static ReusableComposeNode<T> Get<T>() where T : VisualElement
    {
        return ReusableComposeNode<T>.Get();
    }
}

internal class ReusableComposeNode<T> : IDisposable where T : VisualElement
{
    private static readonly NewObjectPool<ReusableComposeNode<T>> _pool = new NewObjectPool<ReusableComposeNode<T>>(
        factory: () => new ReusableComposeNode<T>()
    );

    public T? VisualElement;
    private int _indexInParent = -1;
    private ModifiersPair _lastModifiersPair;
    private IModifier? _lastModifier;
    private Action<T>? _lastInitializer;
    private StyleTranslate _lastTranslate;
    private StyleScale _lastScale;

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
        ModifiersPair modifiers,
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

        if (_lastModifiersPair != modifiers || !Equals(_lastModifier, modifier))
        {
            var newModifier = modifier.OrEmpty();
            if (modifiers.Before != null)
                newModifier = modifiers.Before.Then(newModifier);
            if (modifiers.After != null)
                newModifier = newModifier.Then(modifiers.After);

            newModifier.Apply(_newProperties);
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
            _lastModifiersPair = modifiers;
            _lastModifier = newModifier;
            newModifier.Apply(VisualElement);
        }

        if (initializer != null && _lastInitializer != initializer)
        {
            initializer(VisualElement);
            _lastInitializer = initializer;
        }

        // var callback = VisualElement.OnGloballyPositionedCallbackOrNull();
        // if (callback == null || callback.InvokedAtFrame >= Time.frameCount)
        //     return;
        // var style = VisualElement.style;
        // if (_lastTranslate != style.translate || _lastScale != style.scale)
        // {
        //     _lastTranslate = style.translate;
        //     _lastScale = style.scale;
        //     callback.ReInvoke();
        // }
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
        _lastModifiersPair = new ModifiersPair();
        _lastModifier = null;
        _lastInitializer = null;
        _lastTranslate = StyleKeyword.Null;
        _lastTranslate = StyleKeyword.Null;
        
        _pool.Release(this);
    }
}