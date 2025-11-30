namespace Packages.UnityCompose.Impl.SlotTableModels.Models;

public readonly record struct ComposeGroup(
    int Key,
    ComposeGroupType Type,
    int ParentAnchorId,
    int Size,
    int DataAnchorId
);

public enum ComposeGroupType
{
    Root,
    Replace,
    Restartable,
}