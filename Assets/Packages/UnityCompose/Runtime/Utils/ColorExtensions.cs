// ReSharper disable CheckNamespace

using System;
using SystemColor = System.Drawing.Color;
using UnityColor = UnityEngine.Color;

namespace UnityCompose;

public static class ColorExtensions
{
    public static SystemColor ToSystemColor(this UnityColor color)
    {
        return SystemColor.FromArgb(
            red: color.r.RemapToInt(),
            green: color.g.RemapToInt(),
            blue: color.b.RemapToInt(),
            alpha: color.a.RemapToInt()
        );
    }
    
    public static UnityColor ToUnityColor(this SystemColor color)
    {
        return new UnityColor(
            r: color.R.RemapToFloat(),
            g: color.G.RemapToFloat(),
            b: color.B.RemapToFloat(),
            a: color.A.RemapToFloat()
        );
    }

    private static int RemapToInt(this float component)
    {
        return (int)MathF.Round(
            Math.Clamp(component, 0f, 1f) * 255f
        );
    }

    private static float RemapToFloat(this byte component)
    {
        return Math.Clamp(component, (byte)0, (byte)255) / 255f;
    }
}