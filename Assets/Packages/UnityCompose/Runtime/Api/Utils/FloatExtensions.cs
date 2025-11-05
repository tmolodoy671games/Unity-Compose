using System;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class FloatExtensions
{
    internal static float Approximate(this float value) => value.Round(0.1f);
        
    public static float Round(this float value, float step)
    {
        var stepMultiplier = 1f / step;
        return (float)Math.Round(value * stepMultiplier) / stepMultiplier;
    }
        
    internal static Vector2 Approximate(this Vector2 value) => new(value.x.Approximate(), value.y.Approximate());
}