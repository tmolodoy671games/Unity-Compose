using System.Collections.Generic;
using System.Text;

namespace Packages.UnityCompose.Impl.SlotTableModels.Models;

public class SlotTable
{
    public readonly List<ComposeGroup> Groups = new();
    public readonly List<object?> Slots = new();
    public readonly List<Anchor> Anchors = new();
}