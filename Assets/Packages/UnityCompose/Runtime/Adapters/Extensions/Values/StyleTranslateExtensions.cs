using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;

internal static class StyleTranslateExtensions
{
    public static StyleTranslate CompareAndSetNull(this StyleTranslate value, StyleTranslate compareTo)
    {
        return value == compareTo ? StyleKeyword.Null : value;
    }
}