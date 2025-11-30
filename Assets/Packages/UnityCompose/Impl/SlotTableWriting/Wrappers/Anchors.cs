using System.Collections.Generic;
using System.Text;
using Packages.UnityCompose.Impl.SlotTableModels.Models;

namespace Packages.UnityCompose.Impl.SlotTableWriting.Wrappers;

internal readonly struct Anchors
{
    private readonly List<Anchor> _anchors;

    public Anchors(List<Anchor> anchors)
    {
        _anchors = anchors;
    }
    
    public Anchor this[int index] => _anchors[index];
    
    public int AllocateAnchor(int initialLocation)
    {
        var newAnchorId = _anchors.Count;
       _anchors.Add(new Anchor(initialLocation));
       return newAnchorId;
    }
    
    

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