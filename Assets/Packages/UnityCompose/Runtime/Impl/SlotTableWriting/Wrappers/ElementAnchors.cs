using System.Collections.Generic;
using System.Text;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal readonly struct ElementAnchors
{
    private readonly List<ElementAnchor> _anchors;
    private readonly Stack<ElementAnchorId> _releasedAnchorIds;

    public ElementAnchors(List<ElementAnchor> anchors, Stack<ElementAnchorId> releasedAnchorIds)
    {
        _anchors = anchors;
        _releasedAnchorIds = releasedAnchorIds;
    }
    
    public int Count => _anchors.Count;

    public ElementAnchor this[int index]
    {
        get => _anchors[index];
        set => _anchors[index] = value;
    }

    public ElementAnchor this[ElementAnchorId id]
    {
        get => _anchors[id.Index];
        set => _anchors[id.Index] = value;
    }

    public ElementAnchorId Allocate(VisualElement? parent, int index)
    {
        if (_releasedAnchorIds.TryPop(out var releasedAnchorId))
        {
            this[releasedAnchorId] = new ElementAnchor(parent, index);
            return releasedAnchorId;
        }

        var anchorId = new ElementAnchorId(_anchors.Count);
        _anchors.Add(new ElementAnchor(parent, index));
        return anchorId;
    }

    public void Release(ElementAnchorId anchorId)
    {
        this[anchorId] = ElementAnchor.None;
        _releasedAnchorIds.Push(anchorId);
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        for (var i = 0; i < _anchors.Count; i++)
        {
            var anchor = _anchors[i];
            if (!anchor.IsValid)
                continue;
            builder.Append($"[{i}]\t{anchor}");
        }
        return builder.ToString();
    }
}