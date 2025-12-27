namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;

public readonly record struct ElementAnchorId(int Index)
{
    public static readonly ElementAnchorId None = new(-1);
    
    public bool IsValid => Index >= 0;
    
    public override string ToString()
    {
        if (!IsValid)
            return "None";
        return $"ElementAnchorId({Index})";
    }
}