using System.Collections.Generic;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

internal class SlotTable
{
    public readonly GapBufferList<ComposeGroup> Groups = new();
    public readonly GapBufferList<object?> Slots = new();
    public readonly List<Anchor> GroupsAnchors = new();
    public readonly Stack<AnchorId> FreedGroupAnchors = new();
    public readonly List<Anchor> SlotsAnchors = new();
    public readonly Stack<AnchorId> FreedSlotAnchors = new();
}