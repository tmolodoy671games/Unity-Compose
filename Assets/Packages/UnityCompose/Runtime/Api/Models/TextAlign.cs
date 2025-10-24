// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityCompose;

public enum TextAlign
{
    UpperLeft,
    UpperCenter,
    UpperRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    LowerLeft,
    LowerCenter,
    LowerRight,
}

internal static partial class TextAlignExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TextAnchor ToTextAnchor(this TextAlign textAlign)
    {
        return textAlign switch
        {
            TextAlign.UpperLeft => TextAnchor.UpperLeft,
            TextAlign.UpperCenter => TextAnchor.UpperCenter,
            TextAlign.UpperRight => TextAnchor.UpperRight,
            TextAlign.MiddleLeft => TextAnchor.MiddleLeft,
            TextAlign.MiddleCenter => TextAnchor.MiddleCenter,
            TextAlign.MiddleRight => TextAnchor.MiddleRight,
            TextAlign.LowerLeft => TextAnchor.LowerLeft,
            TextAlign.LowerCenter => TextAnchor.LowerCenter,
            TextAlign.LowerRight => TextAnchor.LowerRight,
            _ => throw new ArgumentOutOfRangeException(nameof(textAlign), textAlign, null)
        };
    }
}