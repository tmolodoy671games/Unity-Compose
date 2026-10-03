// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public class UnityReusableComposeNode : IReusableComposeNode
{
    public readonly VisualElement VisualElement;
    private AnchorManager? _anchorManager;

    private ContentContainer? _contentContainerInstance;
    private ContentContainer? _contentContainer;

    private Shadow? _shadowInstance;
    private Shadow? _shadow;

    private Root? _rootInstance;
    private Root? _root;

    private DrawOn? _drawOnInstance;
    private DrawOn? _drawOn;

    public UnityReusableComposeNode(VisualElement visualElement) : this(visualElement, false)
    {
    }

    private VisualElement ContentContainer => _contentContainer ?? VisualElement;
    public VisualElement Root => _root ?? VisualElement;

    internal UnityReusableComposeNode(VisualElement visualElement, bool isRoot)
    {
        IsRoot = isRoot;
        VisualElement = visualElement;
        VisualElement.pickingMode = PickingMode.Ignore;
        visualElement.SetReusableComposeNode(this);
    }

    public void Remove(IReusableComposeNode child)
    {
        var childVisualElement = child.Root();
        ContentContainer.Remove(childVisualElement);
    }

    public void FastRemove(int index, IReusableComposeNode child)
    {
        var childVisualElement = child.Root();
        if (childVisualElement.parent != VisualElement)
            return;
        if (ContentContainer.GetOrNull(index) == childVisualElement)
        {
            ContentContainer.RemoveAt(index);
            return;
        }

        ContentContainer.Remove(childVisualElement);
    }

    public void Insert(int index, IReusableComposeNode child)
    {
        ContentContainer.Insert(index, child.Root());
    }

    public void Reinsert(int index, IReusableComposeNode child)
    {
        var childVisualElement = child.Root();
        var parent = ContentContainer;
        if (parent.GetOrNull(index) == childVisualElement)
            return;
        childVisualElement.RemoveFromHierarchy();
        parent.Insert(index, childVisualElement);
    }

    public AnchorManager RequireAnchorManager()
    {
        if (_anchorManager == null)
            _anchorManager = AnchorManager.Get();
        return _anchorManager;
    }

    public bool IsRoot { get; }
    public IReusableComposeNode? Parent => VisualElement.parent?.GetReusableComposeNode();
    public int IndexInParent => VisualElement.parent.IndexOf(VisualElement);
    public AnchorManager? AnchorManager => _anchorManager;

    public VisualElement SetupDrawOn()
    {
        if (_drawOn != null)
            return _drawOn;
        _drawOnInstance ??= new DrawOn
        {
            pickingMode = PickingMode.Ignore,
            style =
            {
                width = new Length(100, LengthUnit.Percent),
                height = new Length(100, LengthUnit.Percent),
                position = Position.Absolute
            }
        };
        _drawOn = _drawOnInstance;
        VisualElement.Insert(VisualElement.childCount, _drawOn);
        return _drawOn;
    }

    public void RemoveDrawOn()
    {
        if (_drawOn == null)
            return;
        if (VisualElement.GetOrNull(VisualElement.childCount - 1) == _drawOn)
            VisualElement.RemoveAt(VisualElement.childCount - 1);
        else
            VisualElement.Remove(_drawOn);
        _drawOn = null;
    }

    VisualElement SetupRoot()
    {
        if (_root != null)
            return _root;
        _rootInstance ??= new Root { pickingMode = PickingMode.Ignore };
        _root = _rootInstance;

        var parent = VisualElement.parent;
        var indexInParent = parent.IndexOf(VisualElement);
        parent.RemoveAt(indexInParent);
        _root.Add(VisualElement);
        parent.Insert(indexInParent, _root);
        return _root;
    }

    public void RemoveRoot()
    {
        if (_root == null)
            return;
        var parent = _root.parent;
        var indexInParent = parent.IndexOf(_root);
        _root.Remove(VisualElement);
        parent.RemoveAt(indexInParent);
        parent.Insert(indexInParent, VisualElement);
        _root = null;
    }

    public VisualElement SetupShadow()
    {
        if (_shadow != null)
            return _shadow;
        _shadowInstance ??= new Shadow { pickingMode = PickingMode.Ignore };
        _shadow = _shadowInstance;
        SetupRoot();
        _root.NotNull().Insert(0, _shadow);
        return _shadow;
    }

    public void RemoveShadow()
    {
        if (_shadow == null)
            return;
        SetupRoot();
        _root.NotNull().Remove(_shadow);
        if (_root.NotNull().childCount == 1)
            RemoveRoot();
        _shadow = null;
    }

    public VisualElement SetupContentContainer()
    {
        if (_contentContainer != null)
            return _contentContainer;
        _contentContainerInstance ??= new ContentContainer { pickingMode = PickingMode.Ignore };
        _contentContainer = _contentContainerInstance;
        var children = VisualElement.Children().ToImmutableStableList();
        foreach (var child in children)
            child.parent.Remove(child);
        VisualElement.Insert(0, _contentContainerInstance);
        foreach (var child in children)
            _contentContainerInstance.Add(child);
        return _contentContainer;
    }

    public void RemoveContentContainer()
    {
        if (_contentContainer == null)
            return;
        var children = _contentContainer.Children().ToImmutableStableList();
        foreach (var child in children)
            child.parent.Remove(child);
        VisualElement.Remove(_contentContainer);
        foreach (var child in children)
            VisualElement.Add(child);
        _contentContainer = null;
    }

    public override string ToString()
    {
        return VisualElement.GetType().Name;
    }
}

public static class ReusableComposeNodeExtensions
{
    public static VisualElement VisualElement(this IReusableComposeNode node)
    {
        return node.CastTo<UnityReusableComposeNode>().VisualElement;
    }

    public static VisualElement Root(this IReusableComposeNode node)
    {
        return node.CastTo<UnityReusableComposeNode>().Root;
    }

    public static T VisualElement<T>(this IReusableComposeNode node) where T : VisualElement
    {
        return node.CastTo<UnityReusableComposeNode>().VisualElement.CastTo<T>();
    }
}

internal static class ReusableNodeVisualElementExtensions
{
    private const string ReusableComposeNodeKey = "ReusableComposeNode";

    internal static IReusableComposeNode? GetReusableComposeNode(this VisualElement visualElement)
    {
        return visualElement.UserData().GetOrDefault(ReusableComposeNodeKey, null) as IReusableComposeNode;
    }

    internal static void SetReusableComposeNode(
        this VisualElement visualElement,
        IReusableComposeNode node
    )
    {
        visualElement.UserData()[ReusableComposeNodeKey] = node;
    }
}

internal class Root : VisualElement
{
}

internal class DrawOn : VisualElement
{
}

internal class Shadow : VisualElement
{
}

internal class ContentContainer : VisualElement
{
}