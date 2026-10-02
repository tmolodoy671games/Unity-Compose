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
    private VisualElement? _contentContainer;
    private VisualElement? _root;
    private DrawOn? _drawOnInstance;
    private DrawOn? _drawOn;

    public UnityReusableComposeNode(VisualElement visualElement) : this(visualElement, false)
    {
    }

    public VisualElement ContentContainer => _contentContainer ?? VisualElement;
    public VisualElement Root => _root ?? VisualElement;
    public VisualElement DrawOn => _drawOn ?? throw new ArgumentException();

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

    public void SetupDrawOn()
    {
        if (_drawOnInstance == null)
        {
            _drawOnInstance = new DrawOn
            {
                style =
                {
                    width = new Length(100, LengthUnit.Percent),
                    height = new Length(100, LengthUnit.Percent),
                    position = Position.Absolute
                }
            };
        }
        if (_drawOn != null)
            return;
        _drawOn = _drawOnInstance;
        VisualElement.Insert(VisualElement.childCount, _drawOn);
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

    public void SetContentContainer(VisualElement contentContainer)
    {
        if (_contentContainer != null)
            return;
        var children = VisualElement.Children().ToImmutableStableList();
        foreach (var child in children)
            child.parent.Remove(child);
        VisualElement.Insert(0, contentContainer);
        foreach (var child in children)
            contentContainer.Add(child);
        _contentContainer = contentContainer;
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

    public void SetRoot(VisualElement root)
    {
        if (_root != null)
            return;
        var parent = Root.parent;
        var indexInParent = parent.IndexOf(Root);
        parent.RemoveAt(indexInParent);
        root.Add(Root);
        parent.Insert(indexInParent, root);
        _root = root;
    }

    public void RemoveRoot()
    {
        if (_root == null)
            return;
        var parent = Root.parent;
        var indexInParent = parent.IndexOf(Root);
        Root.Remove(VisualElement);
        parent.RemoveAt(indexInParent);
        parent.Insert(indexInParent, VisualElement);
        _root = null;
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

    public static VisualElement ContentContainer(this IReusableComposeNode node)
    {
        return node.CastTo<UnityReusableComposeNode>().ContentContainer;
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

internal class DrawOn : VisualElement
{
}