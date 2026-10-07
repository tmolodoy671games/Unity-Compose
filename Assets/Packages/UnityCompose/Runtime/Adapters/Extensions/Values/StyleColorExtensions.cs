using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;

internal static class StyleColorExtensions
{
    public static StyleColor CompareAndSetNull(this StyleColor value, StyleColor compareTo)
    {
        return value == compareTo ? StyleKeyword.Null : value;
    }
}