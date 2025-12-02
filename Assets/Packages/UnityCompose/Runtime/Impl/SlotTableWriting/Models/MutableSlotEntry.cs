namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Models;

internal class MutableSlotEntry<T>
{
    public T Value;

    public MutableSlotEntry(T value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"MutableSlotEntry({Value})";
    }
}