using SharpExtensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Wrappers;

internal static class MovableGroup
{
    public const int MetadataSize = 1;
    public const int DataKeyOffset = 0;
}

internal static class KeyGroupSlotsExtensions
{
    public static Optional<T> GetKey<T>(this Slots slots, int slotIndex)
    {
        return slots.GetAsStruct<T>(slotIndex + MovableGroup.DataKeyOffset);
    }

    public static void SetKey<T>(this Slots slots, int slotIndex, T value)
    {
        slots.SetAsStruct(slotIndex + MovableGroup.DataKeyOffset, value);
    }

    public static void InsertKey<T>(this Slots slots, int slotIndex, T value)
    {
        slots.InsertAsStruct(slotIndex + MovableGroup.DataKeyOffset, value);
    }
}