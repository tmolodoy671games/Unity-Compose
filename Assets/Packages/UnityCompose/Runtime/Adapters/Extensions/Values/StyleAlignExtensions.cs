using System;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;

internal static class StyleAlignExtensions
{
    public static StyleEnum<T> CompareAndSetNull<T>(this StyleEnum<T> value, StyleEnum<T> compareTo)
        where T : struct, IConvertible
    {
        return value == compareTo ? StyleKeyword.Null : value;
    }
}