namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

internal readonly struct Anchor
{
    public static readonly Anchor None = new(-1);
    
    public readonly int Location;

    public Anchor(int location)
    {
        Location = location;
    }

    public bool IsValid => Location >= 0;

    public override string ToString()
    {
        if (!IsValid)
            return "None";
        return $"Anchor({Location})";
    }
}