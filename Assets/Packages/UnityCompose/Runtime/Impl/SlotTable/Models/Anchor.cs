namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

internal readonly struct Anchor
{
    public readonly int Index;

    public Anchor(int index)
    {
        Index = index;
    }

    public bool IsValid => Index != int.MaxValue;

    public override string ToString()
    {
        return $"Anchor({Index})";
    }
}