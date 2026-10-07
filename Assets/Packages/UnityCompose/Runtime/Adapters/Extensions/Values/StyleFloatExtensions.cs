using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;

internal static class StyleFloatExtensions
{
    public static StyleFloat CompareAndSetNull(this StyleFloat value, StyleFloat compareTo)
    {
        return value == compareTo ? StyleKeyword.Null : value;
    }
}