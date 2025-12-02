using System.Collections.Generic;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

internal class SlotTable
{
    public readonly List<ComposeGroup> Groups = new();
    public readonly List<object?> Slots = new();
    public readonly List<Anchor> GroupsAnchors = new();
    public readonly List<Anchor> SlotsAnchors = new();
}