using System.Collections.Generic;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.SlotTableWriting.Extensions;

internal static class ListExtensions
{
    public static void Move<T>(
        this GapBufferList<T> list,
        List<T> buffer,
        int startIndex,
        int targetIndex,
        int count
    )
    {
        if (count == 0)
            return;

        buffer.Clear();
        for (var i = startIndex; i < startIndex + count; i++)
            buffer.Add(list[i]);

        list.RemoveRange(startIndex, count);
        list.InsertRange(targetIndex, buffer);
        buffer.Clear();
    }
}