using System.Collections.Generic;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

internal class SlotTable
{
    public readonly GapBufferList<ComposeGroup> Groups = new();
    public readonly GapBufferList<object?> Slots = new();
    public readonly List<Anchor> GroupsAnchors = new();
    public readonly Stack<AnchorId> ReleasedGroupAnchors = new();
    public readonly List<Anchor> SlotsAnchors = new();
    public readonly Stack<AnchorId> ReleasedSlotAnchors = new();
    public readonly List<ElementAnchor> ElementAnchors = new();
    public readonly Stack<ElementAnchorId> ReleasedElementAnchors = new();
}