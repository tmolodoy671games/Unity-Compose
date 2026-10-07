using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;

internal static class StyleLengthExtensions
{
    public static StyleLength CompareAndSetNull(this StyleLength value, StyleLength compareTo)
    {
        return value == compareTo ? StyleKeyword.Null : value;
    }
}