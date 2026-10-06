using UnityEngine;

// ReSharper disable CheckNamespace

namespace DesignSystem;

public static class UnityColorExtensions
{
    public static Color With(
        this Color color,
        float r = -1f,
        float g = -1f,
        float b = -1f,
        float a = -1f,
        float h = -1f,
        float v = -1f,
        float s = -1f
    )
    {
        if (h >= 0f || s >= 0f || v >= 0f)
        {
            Color.RGBToHSV(color, out var currentH, out var currentS, out var currentV);

            h = h >= 0f ? h : currentH;
            s = s >= 0f ? s : currentS;
            v = v >= 0f ? v : currentV;

            color = Color.HSVToRGB(h, s, v);
        }

        return new Color(
            r >= 0f ? r : color.r,
            g >= 0f ? g : color.g,
            b >= 0f ? b : color.b,
            a >= 0f ? a : color.a);
    }

    public static float H(this Color color)
    {
        Color.RGBToHSV(color, out var h, out _, out _);
        return h;
    }

    public static float S(this Color color)
    {
        Color.RGBToHSV(color, out _, out var s, out _);
        return s;
    }

    public static float V(this Color color)
    {
        Color.RGBToHSV(color, out _, out _, out var v);
        return v;
    }
}