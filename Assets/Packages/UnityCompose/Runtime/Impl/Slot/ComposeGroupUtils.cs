using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Slot;

internal static class ComposeGroupUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetKey(string filePath, int lineNumber)
    {
        if (filePath.Length == 0)
            return lineNumber;
        // return 31 * RuntimeHelpers.GetHashCode(filePath) + lineNumber;
        return 31 * filePath.GetHashCode() + lineNumber;
    }
}