namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTable.Models;

internal readonly record struct ComposeGroup(
    int Key,
    ComposeGroupType Type,
    AnchorId ParentAnchorId,
    AnchorId AnchorId,
    int Size,
    AnchorId DataAnchorId,
    int SlotsSize,
    int ElementIndex,
    int ElementsCount
);

internal enum ComposeGroupType : byte
{
    Replace,
    Restart,
    Reusable,
}