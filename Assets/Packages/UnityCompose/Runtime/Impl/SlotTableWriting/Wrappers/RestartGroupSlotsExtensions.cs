using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableModels;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Entities;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal static class RestartGroup
{
    public const int MetadataSize = 2;
    public const int PreviousStateOffset = 0;
    public const int RestartScopeOffset = 1;
}

internal static class PreviousStateSlotsExtensions
{
    public static Optional<T> GetPreviousState<T>(this Slots slots, int dataIndex)
    {
        return slots.GetAsOptional<T>(dataIndex + RestartGroup.PreviousStateOffset);
    }

    public static Optional<T> GetPreviousStateAsStruct<T>(this Slots slots, int dataIndex) where T : struct
    {
        return slots.GetAsStruct<T>(dataIndex + RestartGroup.PreviousStateOffset);
    }

    public static void SetPreviousState<T>(this Slots slots, int dataIndex, T previousState)
    {
        slots[dataIndex + RestartGroup.PreviousStateOffset] = previousState;
    }

    public static void SetPreviousStateAsStruct<T>(this Slots slots, int dataIndex, T previousState) where T : struct
    {
        slots.SetAsStruct(dataIndex + RestartGroup.PreviousStateOffset, previousState);
    }

    public static void InsertPreviousState(this Slots slots, int dataIndex)
    {
        slots.Insert(dataIndex + RestartGroup.PreviousStateOffset, ComposeEmptySlot.Instance);
    }
}

internal static class RestartScopeSlotsExtensions
{
    public static ComposeRestartScope? GetRestartScope(this Slots slots, int dataIndex)
    {
        return slots.Get<ComposeRestartScope>(dataIndex + RestartGroup.RestartScopeOffset);
    }

    public static void SetRestartScope(this Slots slots, int dataIndex, ComposeRestartScope? restartScope)
    {
        slots[dataIndex + RestartGroup.RestartScopeOffset] = restartScope;
    }

    public static void InsertRestartScope(this Slots slots, int dataIndex)
    {
        slots.Insert(dataIndex + RestartGroup.RestartScopeOffset, ComposeEmptySlot.Instance);
    }
}