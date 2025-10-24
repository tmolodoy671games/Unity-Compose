// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;

namespace UnityCompose;

public enum FontStyle
{
    Normal,
    Italic
}

internal static partial class FontStyleUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static UnityEngine.FontStyle ToUnityFontStyle(FontStyle fontStyle, FontWeight fontWeight)
    {
        if (fontStyle == FontStyle.Italic && fontWeight == FontWeight.Bold)
            return UnityEngine.FontStyle.BoldAndItalic;
        if (fontStyle == FontStyle.Normal && fontWeight == FontWeight.Bold)
            return UnityEngine.FontStyle.Bold;
        if (fontStyle == FontStyle.Italic && fontWeight == FontWeight.Normal)
            return UnityEngine.FontStyle.Italic;
        return UnityEngine.FontStyle.Normal;
    }
}