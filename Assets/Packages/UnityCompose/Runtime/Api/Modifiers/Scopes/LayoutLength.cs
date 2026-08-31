// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

public readonly struct LayoutLength : IEquatable<LayoutLength>
{
    private readonly Length _value;

    private LayoutLength(Dp dp) : this()
    {
        _value = dp.ToLength();
        HasValue = true;
    }

    private LayoutLength(Percent percent) : this()
    {
        _value = percent.ToLength();
        HasValue = true;
    }

    public bool HasValue { get; }

    internal Length ToLength()
    {
        return _value;
    }

    public bool Equals(LayoutLength other)
    {
        return HasValue == other.HasValue && _value == other._value;
    }

    public override bool Equals(object? obj)
    {
        return obj is LayoutLength other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(HasValue, _value);
    }

    public static implicit operator LayoutLength(Dp dp) => new(dp);

    public static implicit operator LayoutLength(Percent percent) => new(percent);
    
    public static bool operator ==(LayoutLength lhs, LayoutLength rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(LayoutLength lhs, LayoutLength rhs)
    {
        return !(lhs == rhs);
    }

    public static implicit operator Length(LayoutLength length)
    {
        return length.ToLength();
    }
    
    public static implicit operator StyleLength(LayoutLength length)
    {
        return length.ToLength();
    }

    public override string ToString()
    {
        return _value.unit switch
        {
            LengthUnit.Pixel => _value.value.Dp().ToString(),
            LengthUnit.Percent => _value.value.Percent().ToString(),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public static LayoutLength Lerp(LayoutLength a, LayoutLength b, float t)
    {
        if (!a.HasValue || !b.HasValue)
            throw new ArgumentException("No value present!");
        if (a._value.unit == LengthUnit.Pixel && b._value.unit == LengthUnit.Pixel)
        {
            return Dp.Lerp(a._value.value.Dp(), b._value.value.Dp(), t);
        }
        if (a._value.unit == LengthUnit.Percent && b._value.unit == LengthUnit.Percent)
        {
            return Percent.Lerp(a._value.value.Percent(), b._value.value.Percent(), t);
        }

        throw new ArgumentException("Not of the same type!");
    }

    public static LayoutLength LerpUnclamped(LayoutLength a, LayoutLength b, float t)
    {
        if (!a.HasValue || !b.HasValue)
            throw new ArgumentException("No value present!");
        if (a._value.unit == LengthUnit.Pixel && b._value.unit == LengthUnit.Pixel)
        {
            return Dp.LerpUnclamped(a._value.value.Dp(), b._value.value.Dp(), t);
        }
        if (a._value.unit == LengthUnit.Percent && b._value.unit == LengthUnit.Percent)
        {
            return Percent.LerpUnclamped(a._value.value.Percent(), b._value.value.Percent(), t);
        }

        throw new ArgumentException("Not of the same type!");
    }
}

public readonly struct Dp : IEquatable<Dp>
{
    public readonly float Value;

    public Dp(float value)
    {
        Value = value;
    }

    internal Length ToLength() => new(Value, LengthUnit.Pixel);

    public bool Equals(Dp other)
    {
        return Value.AlmostEquals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Dp other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString()
    {
        return $"{Value}px";
    }
    
    public static Dp operator -(Dp left)
    {
        return new Dp(-left.Value);
    }

    public static Dp operator +(Dp left, Dp right)
    {
        return new Dp(left.Value + right.Value);
    }

    public static Dp operator -(Dp left, Dp right)
    {
        return new Dp(left.Value - right.Value);
    }

    public static Dp operator *(Dp left, Dp right)
    {
        return new Dp(left.Value * right.Value);
    }
    
    public static Dp operator *(float left, Dp right)
    {
        return new Dp(left * right.Value);
    }
    
    public static Dp operator *(Dp left, float right)
    {
        return new Dp(left.Value * right);
    }
    
    public static Dp operator *(int left, Dp right)
    {
        return new Dp(left * right.Value);
    }
    
    public static Dp operator *(Dp left, int right)
    {
        return new Dp(left.Value * right);
    }
    
    public static Dp operator /(Dp left, Dp right)
    {
        return new Dp(left.Value / right.Value);
    }
    
    public static Dp operator /(Dp left, float right)
    {
        return new Dp(left.Value / right);
    }
    
    public static Dp operator /(Dp left, int right)
    {
        return new Dp(left.Value / right);
    }

    public static bool operator ==(Dp lhs, Dp rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Dp lhs, Dp rhs)
    {
        return !(lhs == rhs);
    }

    public static Dp Lerp(Dp a, Dp b, float t)
    {
        return new Dp(Mathf.Lerp(a.Value, b.Value, t));
    }

    public static Dp LerpUnclamped(Dp a, Dp b, float t)
    {
        return new Dp(Mathf.LerpUnclamped(a.Value, b.Value, t));
    }
}

public readonly struct Percent : IEquatable<Percent>
{
    public readonly float Value;

    public Percent(float value)
    {
        Value = value;
    }

    internal Length ToLength() => new(Value, LengthUnit.Percent);

    public bool Equals(Percent other)
    {
        return Value.AlmostEquals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Percent other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString()
    {
        return $"{Value}%";
    }
    
    public static Percent operator *(float left, Percent right) {
        return new Percent(left * right.Value);
    }
    
    public static Percent operator /(Percent left, float right) {
        return new Percent(left.Value / right);
    }

    public static Percent operator +(Percent left, Percent right)
    {
        return new Percent(left.Value + right.Value);
    }

    public static Percent operator -(Percent left, Percent right)
    {
        return new Percent(left.Value - right.Value);
    }
    
    public static Percent operator -(Percent left)
    {
        return new Percent(-left.Value);
    }

    public static Percent operator *(Percent left, Percent right)
    {
        return new Percent(left.Value * right.Value);
    }

    public static Percent operator *(Percent left, float right)
    {
        return new Percent(left.Value * right);
    }

    public static Percent operator /(Percent left, Percent right)
    {
        return new Percent(left.Value / right.Value);
    }

    public static bool operator ==(Percent lhs, Percent rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Percent lhs, Percent rhs)
    {
        return !(lhs == rhs);
    }

    public static Percent Lerp(Percent a, Percent b, float t)
    {
        return new Percent(Mathf.Lerp(a.Value, b.Value, t));
    }

    public static Percent LerpUnclamped(Percent a, Percent b, float t)
    {
        return new Percent(Mathf.LerpUnclamped(a.Value, b.Value, t));
    }
}

public static partial class FloatExtensions
{
    public static Percent Percent(this float coordinate) => new(coordinate);

    public static Dp Dp(this float value) => new Dp(value);


    public static Percent Percent(this int coordinate) => new(coordinate);

    public static Dp Dp(this int value) => new Dp(value);
}