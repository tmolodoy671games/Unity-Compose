using System;
using System.Drawing;

// ReSharper disable CheckNamespace

namespace DesignSystem;

public static class ColorExtensions
{
    public static float H(this Color color)
    {
        RgbToHsv(color.R, color.G, color.B, out var h, out _, out _);
        return h;
    }

    public static float S(this Color color)
    {
        RgbToHsv(color.R, color.G, color.B, out _, out var s, out _);
        return s;
    }

    public static float V(this Color color)
    {
        RgbToHsv(color.R, color.G, color.B, out _, out _, out var v);
        return v;
    }
    
    public static Color With(
        this Color color,
        int r = -1,
        int g = -1,
        int b = -1,
        int a = -1,
        float h = -1f,
        float v = -1f,
        float s = -1f
    )
    {
        if (h >= 0f || s >= 0f || v >= 0f)
        {
            RgbToHsv(color.R, color.G, color.B, out var currentH, out var currentS, out var currentV);

            h = h >= 0f ? h : currentH;
            s = s >= 0f ? s : currentS;
            v = v >= 0f ? v : currentV;

            HsvToRgb(h, s, v, out var hsvR, out var hsvG, out var hsvB);

            r = hsvR;
            g = hsvG;
            b = hsvB;
        }

        return Color.FromArgb(
            a >= 0 ? a : color.A,
            r >= 0 ? r : color.R,
            g >= 0 ? g : color.G,
            b >= 0 ? b : color.B);
    }

    private static void RgbToHsv(
        byte r,
        byte g,
        byte b,
        out float h,
        out float s,
        out float v)
    {
        var rf = r / 255f;
        var gf = g / 255f;
        var bf = b / 255f;

        var max = MathF.Max(rf, MathF.Max(gf, bf));
        var min = MathF.Min(rf, MathF.Min(gf, bf));
        var delta = max - min;

        v = max;
        s = max == 0f ? 0f : delta / max;

        if (delta == 0f)
        {
            h = 0f;
            return;
        }

        h = max switch
        {
            var x when x == rf => 60f * ((gf - bf) / delta % 6f),
            var x when x == gf => 60f * ((bf - rf) / delta + 2f),
            _ => 60f * ((rf - gf) / delta + 4f)
        };

        if (h < 0f)
            h += 360f;
    }

    private static void HsvToRgb(
        float h,
        float s,
        float v,
        out int r,
        out int g,
        out int b)
    {
        h %= 360f;
        if (h < 0f)
            h += 360f;

        s = Math.Clamp(s, 0f, 1f);
        v = Math.Clamp(v, 0f, 1f);

        var c = v * s;
        var x = c * (1f - MathF.Abs(h / 60f % 2f - 1f));
        var m = v - c;

        (var rf, var gf, var bf) = h switch
        {
            < 60f => (c, x, 0f),
            < 120f => (x, c, 0f),
            < 180f => (0f, c, x),
            < 240f => (0f, x, c),
            < 300f => (x, 0f, c),
            _ => (c, 0f, x)
        };

        r = (int)((rf + m) * 255f);
        g = (int)((gf + m) * 255f);
        b = (int)((bf + m) * 255f);
    }
}