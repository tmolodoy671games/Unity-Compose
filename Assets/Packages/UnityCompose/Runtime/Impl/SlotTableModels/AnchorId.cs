namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

public readonly record struct AnchorId(int Index)
{
    public static readonly AnchorId None = new(-1);

    public bool IsValid => Index >= 0;

    public override string ToString()
    {
        if (!IsValid)
            return "None";
        return $"AnchorId({Index})";
    }
}