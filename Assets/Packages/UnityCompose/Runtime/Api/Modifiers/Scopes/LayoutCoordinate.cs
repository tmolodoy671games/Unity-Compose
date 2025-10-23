// ReSharper disable CheckNamespace

using System;
using UnityEngine.UIElements;

namespace UnityCompose;

public readonly record struct LayoutCoordinate(
    float coordinate,
    LayoutCoordinate.Unit unit,
    bool HasValue
)
{
    public enum Unit
    {
        Pixel,
        Percent
    }

    internal Length ToLength()
    {
        return new Length(
            coordinate,
            unit switch
            {
                Unit.Pixel => LengthUnit.Pixel,
                Unit.Percent => LengthUnit.Percent,
                _ => throw new ArgumentOutOfRangeException()
            }
        );
    }

    public static implicit operator LayoutCoordinate(float coordinate)
    {
        return new LayoutCoordinate(coordinate, Unit.Pixel, true);
    }
}

public static partial class FloatExtensions
{
    public static LayoutCoordinate Percent(this float coordinate) =>
        new(coordinate, LayoutCoordinate.Unit.Percent, true);
}