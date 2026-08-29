#nullable enable
// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using UnityEngine.UIElements;
using UnityCompose;
using static UnityCompose.ComposeFunctions;

namespace UnityCompose;
public static partial class SpExtensions
{
    public static float __Resolve(this Sp sp, global::UnityCompose.Composer __composer = null !, int __changed = -1)
    {
        var __dirty = __changed;
        if ((__changed & 0b_11) == 0)
            __dirty |= __composer.Changed(sp) ? 0b_10 : 0b_01;
        return sp.Value * LocalTextScale.Current;
    }
}