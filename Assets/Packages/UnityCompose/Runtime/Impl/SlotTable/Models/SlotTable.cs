using System.Collections.Generic;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

internal class SlotTable
{
    public readonly List<ComposeGroup> Groups = new();
    public readonly List<object?> Slots = new();
    public readonly List<Anchor> GroupsAnchors = new();
    public readonly Stack<AnchorId> FreedGroupAnchors = new();
    public readonly List<Anchor> SlotsAnchors = new();
    public readonly Stack<AnchorId> FreedSlotAnchors = new();
}