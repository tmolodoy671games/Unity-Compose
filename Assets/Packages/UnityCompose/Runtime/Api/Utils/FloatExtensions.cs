using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static class FloatExtensions
{
    public static Length Percent(this float value)
    {
        return new Length(value, LengthUnit.Percent);
    }

    internal static float Approximate(this float value) => value.Round(0.1f);
        
    public static float Round(this float value, float step)
    {
        var stepMultiplier = 1f / step;
        return (float)Math.Round(value * stepMultiplier) / stepMultiplier;
    }
        
    internal static Vector2 Approximate(this Vector2 value) => new(value.x.Approximate(), value.y.Approximate());
}

public static class IntExtensions
{
    public static Length Percent(this int value)
    {
        return new Length(value, LengthUnit.Percent);
    }
}