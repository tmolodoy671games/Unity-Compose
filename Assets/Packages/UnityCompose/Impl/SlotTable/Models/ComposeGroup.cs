namespace Packages.UnityCompose.Impl.SlotTable.Models;

public readonly record struct ComposeGroup(
    int Key,
    ComposeGroupType Type,
    int ParentAnchorId,
    int Size,
    int DataAnchorId
);

public enum ComposeGroupType
{
    Replace,
    Restartable,
}