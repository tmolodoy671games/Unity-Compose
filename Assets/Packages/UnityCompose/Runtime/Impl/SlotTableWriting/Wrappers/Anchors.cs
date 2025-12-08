using System.Collections.Generic;
using System.Text;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal readonly struct Anchors
{
    private readonly List<Anchor> _anchors;

    public Anchors(List<Anchor> anchors)
    {
        _anchors = anchors;
    }
    
    public int Count => _anchors.Count;
    
    public Anchor this[AnchorId id]
    {
        get => _anchors[id.Index];
        set => _anchors[id.Index] = value;
    }

    public Anchor this[int id]
    {
        get => _anchors[id];
        set => _anchors[id] = value;
    }

    public AnchorId AllocateAnchor(int initialLocation)
    {
        var newAnchorId = _anchors.Count;
       _anchors.Add(new Anchor(initialLocation));
       return new AnchorId(newAnchorId);
    }
    
    public void Clear() => _anchors.Clear();
    
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