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
    
    public UnityReusableComposeNode(VisualElement visualElement) : this(visualElement, false)
    {
    }
    
    public VisualElement ContentContainer => _contentContainer ?? VisualElement;

    internal UnityReusableComposeNode(VisualElement visualElement, bool isRoot)
    {
        IsRoot = isRoot;
        VisualElement = visualElement;
        VisualElement.pickingMode = PickingMode.Ignore;
        visualElement.SetReusableComposeNode(this);
    }

    public void Remove(IReusableComposeNode child)
    {
        var childVisualElement = child.VisualElement();
        ContentContainer.Remove(childVisualElement);
    }

    public void FastRemove(int index, IReusableComposeNode child)
    {
        var childVisualElement = child.VisualElement();
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
        ContentContainer.Insert(index, child.VisualElement());
    }

    public void Reinsert(int index, IReusableComposeNode child)
    {
        var childVisualElement = child.VisualElement();
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

    public void SetContentContainer(VisualElement contentContainer)
    {
        if (_contentContainer != null)
            return;
        var children = VisualElement.Children().ToImmutableStableList();
        foreach (var child in children)
            child.parent.Remove(child);
        _contentContainer = contentContainer;
        VisualElement.Insert(0, contentContainer);
        foreach (var child in children)
            _contentContainer.Add(child);
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
    }
}

public static class ReusableComposeNodeExtensions
{
    public static VisualElement VisualElement(this IReusableComposeNode node)
    {
        return node.CastTo<UnityReusableComposeNode>().VisualElement;
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