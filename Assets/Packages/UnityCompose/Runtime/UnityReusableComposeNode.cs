// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;
using UnityEngine.UIElements;
using TimeUtils = UnityCompose.Packages.UnityCompose.Runtime.Adapters.Utils.TimeUtils;

namespace UnityCompose;

public class UnityReusableComposeNode : IReusableComposeNode
{
    public readonly VisualElement VisualElement;
    private AnchorManager? _anchorManager;

    private ContentContainer? _contentContainerInstance;
    private ContentContainer? _contentContainer;

    private DrawBehind? _drawBehindInstance;
    private DrawBehind? _drawBehind;

    private Root? _rootInstance;
    private Root? _root;

    private DrawOn? _drawOnInstance;
    private DrawOn? _drawOn;

    private Shadow? _backgroundShadow;
    private Shadow? _backgroundShadowInstance;
    private IVisualElementScheduledItem? _syncBackgroundShadow;
    private Offset _backgroundShadowOffset;

    private Shadow? _foregroundShadow;
    private Shadow? _foregroundShadowInstance;

    public UnityReusableComposeNode(VisualElement visualElement) : this(visualElement, false)
    {
    }

    private VisualElement ContentContainer => _contentContainer ?? VisualElement;
    public VisualElement Root => _root ?? VisualElement;

    public void Dispose()
    {
        foreach (var entry in VisualElement.UserData().Values)
        {
            if (entry is IDisposable disposable and not IReusableComposeNode)
                disposable.Dispose();
        }
    }

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

    public VisualElement SetupDrawOnTop()
    {
        if (_drawOn != null)
            return _drawOn;
        _drawOnInstance ??= new DrawOn
        {
            pickingMode = PickingMode.Ignore,
            style =
            {
                position = Position.Absolute,
                top = 0,
                bottom = 0,
                left = 0,
                right = 0,
            }
        };
        _drawOn = _drawOnInstance;
        VisualElement.Insert(DrawOnIndex(), _drawOn);
        return _drawOn;
    }

    public void RemoveDrawOnTop()
    {
        if (_drawOn == null)
            return;
        if (VisualElement.GetOrNull(DrawOnIndex()) == _drawOn)
            VisualElement.RemoveAt(DrawOnIndex());
        else
            VisualElement.Remove(_drawOn);
        _drawOn = null;
    }

    public VisualElement SetupForegroundShadow()
    {
        if (_foregroundShadow != null)
            return _foregroundShadow;
        _foregroundShadowInstance ??= new Shadow
        {
            pickingMode = PickingMode.Ignore,
            style =
            {
                position = Position.Absolute,
                top = 0,
                bottom = 0,
                left = 0,
                right = 0,
            }
        };
        _foregroundShadow = _foregroundShadowInstance;
        VisualElement.Insert(ForegroundShadowIndex(), _foregroundShadow);
        return _foregroundShadow;
    }

    public void RemoveForegroundShadow()
    {
        if (_foregroundShadow == null)
            return;
        if (VisualElement.GetOrNull(ForegroundShadowIndex()) == _foregroundShadow)
            VisualElement.RemoveAt(ForegroundShadowIndex());
        else
            VisualElement.Remove(_foregroundShadow);
        _foregroundShadow = null;
    }

    public VisualElement SetupRoot()
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

    public VisualElement SetupDrawBehind()
    {
        if (_drawBehind != null)
            return _drawBehind;
        _drawBehindInstance ??= new DrawBehind
        {
            pickingMode = PickingMode.Ignore,
            style =
            {
                position = Position.Absolute,
                top = 0,
                bottom = 0,
                left = 0,
                right = 0,
            }
        };
        _drawBehind = _drawBehindInstance;
        SetupRoot();
        _root.NotNull().Insert(_root.NotNull().childCount - 1, _drawBehind);
        return _drawBehind;
    }

    public void RemoveDrawBehind()
    {
        if (_drawBehind == null)
            return;
        SetupRoot();
        _root.NotNull().Remove(_drawBehind);
        if (_root.NotNull().childCount == 1)
            RemoveRoot();
        _drawBehind = null;
    }

    public VisualElement SetupBackgroundShadow()
    {
        if (_backgroundShadow != null)
            return _backgroundShadow;
        _backgroundShadowInstance ??= new Shadow
        {
            pickingMode = PickingMode.Ignore,
            style =
            {
                position = Position.Absolute,
                top = 0,
                bottom = 0,
                left = 0,
                right = 0,
            }
        };
        _backgroundShadow = _backgroundShadowInstance;
        SetupRoot();
        _root.NotNull().Insert(0, _backgroundShadow);
        _syncBackgroundShadow?.Pause();
        _syncBackgroundShadow = VisualElement.schedule.Execute(SyncBackgroundShadowStyle)
            .Every(TimeUtils.Frametime);
        return _backgroundShadow;
    }

    public void SyncBackgroundShadowOffset(Offset offset)
    {
        _backgroundShadowOffset = offset;
    }

    public void RemoveBackgroundShadow()
    {
        if (_backgroundShadow == null)
            return;
        _syncBackgroundShadow?.Pause();
        SetupRoot();
        _root.NotNull().Remove(_backgroundShadow);
        if (_root.NotNull().childCount == 1)
            RemoveRoot();
        _backgroundShadow = null;
    }

    public VisualElement SetupContentContainer()
    {
        if (_contentContainer != null)
            return _contentContainer;
        _contentContainerInstance ??= new ContentContainer
        {
            pickingMode = PickingMode.Ignore,
        };
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

    private int DrawOnIndex()
    {
        var index = VisualElement.childCount;
        return index;
    }

    private int ForegroundShadowIndex()
    {
        var index = VisualElement.childCount;
        if (_drawOn != null)
            index--;
        return index;
    }

    private void SyncBackgroundShadowStyle()
    {
        if (_backgroundShadow == null)
            return;
        _backgroundShadow.style.scale = VisualElement.style.scale;
        _backgroundShadow.style.rotate = VisualElement.style.rotate;
        _backgroundShadow.style.translate =
            _backgroundShadowOffset.ToVector2() * VisualElement.style.scale.ToVector2() +
            VisualElement.style.translate.ToVector2();
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

internal class DrawBehind : VisualElement
{
}

internal class Shadow : VisualElement
{
}

internal class ContentContainer : VisualElement
{
}