// ReSharper disable CheckNamespace
namespace UnityCompose;

public enum ItemPosition
{
    First,
    Mid,
    Last,
}

public static class ItemPositions
{
    public static ItemPosition Get(int index, int itemsCount)
    {
        if (index == 0) return ItemPosition.First;
        if (index == itemsCount - 1) return ItemPosition.Last;
        return ItemPosition.Mid;
    }
}