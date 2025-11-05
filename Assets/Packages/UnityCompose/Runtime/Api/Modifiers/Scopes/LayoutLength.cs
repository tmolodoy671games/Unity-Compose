// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

public readonly struct LayoutLength : IEquatable<LayoutLength>
{
    private readonly Optional<Px> _px;
    private readonly Optional<Percent> _percent;

    public LayoutLength(Optional<Px> px) : this()
    {
        _px = px;
        _percent = Optional.Empty<Percent>();
    }

    public LayoutLength(Optional<Percent> percent) : this()
    {
        _px = Optional.Empty<Px>();
        _percent = percent;
    }

    public bool HasValue => _px.HasValue || _percent.HasValue;

    internal Length ToLength()
    {
        if (_px.HasValue) return _px.Value.ToLength();
        if (_percent.HasValue) return _percent.Value.ToLength();
        return 0f;
    }

    public bool Equals(LayoutLength other)
    {
        return _px.Equals(other._px) && _percent.Equals(other._percent);
    }

    public override bool Equals(object? obj)
    {
        return obj is LayoutLength other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_px, _percent);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LayoutLength(Px px) => new(px);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LayoutLength(Percent percent) => new(percent);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator LayoutLength(float value) => new Px(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(LayoutLength lhs, LayoutLength rhs)
    {
        return lhs.Equals(rhs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(LayoutLength lhs, LayoutLength rhs)
    {
        return !(lhs == rhs);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Length(LayoutLength length)
    {
        return length.ToLength();
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator StyleLength(LayoutLength length)
    {
        return length.ToLength();
    }
}

public readonly struct Px : IEquatable<Px>
{
    private readonly float _value;

    public Px(float value)
    {
        _value = value;
    }

    internal Length ToLength() => new(_value, LengthUnit.Pixel);

    public bool Equals(Px other)
    {
        return _value.AlmostEquals(other._value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Px other && Equals(other);
    }

    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString()
    {
        return $"{_value}px";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Px(float value) => new Px(value);

    public static Px operator +(Px left, Px right)
    {
        return new Px(left._value + right._value);
    }

    public static Px operator -(Px left, Px right)
    {
        return new Px(left._value - right._value);
    }

    public static Px operator *(Px left, Px right)
    {
        return new Px(left._value * right._value);
    }

    public static Px operator /(Px left, Px right)
    {
        return new Px(left._value / right._value);
    }

    public static bool operator ==(Px lhs, Px rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Px lhs, Px rhs)
    {
        return !(lhs == rhs);
    }
}

public readonly struct Percent : IEquatable<Percent>
{
    private readonly float _value;

    public Percent(float value)
    {
        _value = value;
    }

    internal Length ToLength() => new(_value, LengthUnit.Percent);

    public bool Equals(Percent other)
    {
        return _value.AlmostEquals(other._value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Percent other && Equals(other);
    }

    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString()
    {
        return $"{_value}%";
    }

    public static Percent operator +(Percent left, Percent right)
    {
        return new Percent(left._value + right._value);
    }

    public static Percent operator -(Percent left, Percent right)
    {
        return new Percent(left._value - right._value);
    }

    public static Percent operator *(Percent left, Percent right)
    {
        return new Percent(left._value * right._value);
    }

    public static Percent operator /(Percent left, Percent right)
    {
        return new Percent(left._value / right._value);
    }

    public static bool operator ==(Percent lhs, Percent rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Percent lhs, Percent rhs)
    {
        return !(lhs == rhs);
    }
}

public static partial class FloatExtensions
{
    public static Percent Percent(this float coordinate) => new(coordinate);

    public static Px Px(this float value) => new Px(value);


    public static Percent Percent(this int coordinate) => new(coordinate);

    public static Px Px(this int value) => new Px(value);
}