using System;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;

internal static class StyleScaleExtensions
{
    public static StyleScale CompareAndSetNull(this StyleScale value, StyleScale compareTo)
    {
        return value == compareTo ? StyleKeyword.Null : value;
    }
}