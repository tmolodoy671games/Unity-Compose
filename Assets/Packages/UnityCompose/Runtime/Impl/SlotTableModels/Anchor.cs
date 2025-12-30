namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

internal readonly record struct Anchor(int Location)
{
    public static readonly Anchor None = new(-1);

    public bool IsValid => Location >= 0;

    public override string ToString()
    {
        if (!IsValid)
            return "None";
        return $"Anchor({Location})";
    }
}