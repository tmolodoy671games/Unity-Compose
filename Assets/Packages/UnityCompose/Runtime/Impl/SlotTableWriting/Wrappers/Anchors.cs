using System.Collections.Generic;
using System.Text;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal readonly struct Anchors
{
    private readonly AnchorsType _type;
    private readonly List<Anchor> _anchors;
    private readonly Stack<AnchorId> _releasedAnchorIds;

    public Anchors(AnchorsType type, List<Anchor> anchors, Stack<AnchorId> releasedAnchorIds)
    {
        _type = type;
        _anchors = anchors;
        _releasedAnchorIds = releasedAnchorIds;
    }
    
    public int Count => _anchors.Count;
    
    public Anchor this[AnchorId id]
    {
        get => this[id.Index];
        set => this[id.Index] = value;
    }

    public Anchor this[int id]
    {
        get => _anchors[id];
        set => _anchors[id] = value;
    }

    public AnchorId AllocateAnchor(int initialLocation)
    {
        if (_releasedAnchorIds.TryPop(out var freeAnchorId))
        {
            _anchors[freeAnchorId.Index] = new Anchor(initialLocation);
            return freeAnchorId;
        }
        var newAnchorId = _anchors.Count;
       _anchors.Add(new Anchor(initialLocation));
       return new AnchorId(newAnchorId);
    }

    public void ReleaseAnchor(AnchorId anchorId)
    {
        _releasedAnchorIds.Push(anchorId);
        _anchors[anchorId.Index] = Anchor.None;
    }
    
    public void Clear() => _anchors.Clear();

    public bool ContainsIndex(AnchorId index) => _anchors.ContainsIndex(index.Index);
    
    public override string ToString()
    {
        var builder = new StringBuilder();
        for (var i = 0; i < _anchors.Count; i++)
        {
            var anchor = _anchors[i];
            builder.Append($"[{i}] ");
            builder.AppendLine(anchor.ToString());
        }

        return builder.ToString();
    }
}

internal enum AnchorsType
{
    Groups,
    Slots,
}