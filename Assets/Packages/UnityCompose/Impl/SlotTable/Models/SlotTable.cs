using System.Collections.Generic;

namespace Packages.UnityCompose.Impl.SlotTable.Models;

public class SlotTable
{
    public readonly List<ComposeGroup>  Groups = new();
    public readonly List<object?> Slots = new();
    public readonly List<Anchor> Anchors = new();
}