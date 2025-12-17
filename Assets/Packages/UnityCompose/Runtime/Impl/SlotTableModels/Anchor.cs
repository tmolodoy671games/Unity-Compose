namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

internal readonly struct Anchor
{
    public static readonly Anchor None = new();
    
    public readonly int Index;

    public Anchor(int index)
    {
        Index = index;
    }

    public bool IsValid => Index != int.MaxValue;

    public override string ToString()
    {
        if (!IsValid)
            return "None";
        return $"Anchor({Index})";
    }
}