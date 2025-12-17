namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

internal readonly struct Anchor
{
    public static readonly Anchor None = new(-1);
    
    public readonly int Index;

    public Anchor(int index)
    {
        Index = index;
    }

    public bool IsValid => Index >= 0;

    public override string ToString()
    {
        if (!IsValid)
            return "None";
        return $"Anchor({Index})";
    }
}