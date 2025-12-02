namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

public readonly record struct AnchorId(int Index)
{
    public static readonly AnchorId None = new(int.MaxValue);

    public bool IsValid => Index != int.MaxValue;
}