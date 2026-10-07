using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;

internal static class StyleLengthExtensions
{
    public static StyleLength CompareAndSetNull(this StyleLength value, StyleLength compareTo)
    {
        return value == compareTo ? StyleKeyword.Null : value;
    }

    public static StyleLength Add(this StyleLength value, Length addition)
    {
        StyleLength result = value.value.value + addition.value;
        if (result.value.value.AlmostEquals(0f))
            result = StyleKeyword.Null;
        return result;
    }
    public static StyleLength Subtract(this StyleLength value, Length addition)
    {
        StyleLength result = value.value.value - addition.value;
        if (result.value.value.AlmostEquals(0f))
            result = StyleKeyword.Null;
        return result;
    }
    
}