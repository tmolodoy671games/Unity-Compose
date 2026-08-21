using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal static class RestartGroup
{
    public const int MetadataSize = 1;
    public const int RestartScopeOffset = 0;
}

internal static class RestartScopeSlotsExtensions
{
    public static IComposeRestartScope? GetRestartScope(this Slots slots, int dataIndex)
    {
        return slots.GetAsOptional<IComposeRestartScope>(dataIndex + RestartGroup.RestartScopeOffset).GetOrDefault(null!);
    }

    public static void SetRestartScope(this Slots slots, int dataIndex, IComposeRestartScope? restartScope)
    {
        slots[dataIndex + RestartGroup.RestartScopeOffset] = restartScope;
    }

    public static void InsertRestartScope(this Slots slots, int dataIndex)
    {
        slots.Insert(dataIndex + RestartGroup.RestartScopeOffset, ComposeEmptySlot.Instance);
    }
}