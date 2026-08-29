// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
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
}

public readonly struct Dp : IEquatable<Dp>
{
    private readonly float _value;

    public Dp(float value)
    {
        _value = value;
    }

    internal Length ToLength() => new(_value, LengthUnit.Pixel);

    public bool Equals(Dp other)
    {
        return _value.AlmostEquals(other._value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Dp other && Equals(other);
    }

    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString()
    {
        return $"{_value}px";
    }
    
    public static Dp operator -(Dp left)
    {
        return new Dp(-left._value);
    }

    public static Dp operator +(Dp left, Dp right)
    {
        return new Dp(left._value + right._value);
    }

    public static Dp operator -(Dp left, Dp right)
    {
        return new Dp(left._value - right._value);
    }

    public static Dp operator *(Dp left, Dp right)
    {
        return new Dp(left._value * right._value);
    }
    
    public static Dp operator *(float left, Dp right)
    {
        return new Dp(left * right._value);
    }
    
    public static Dp operator *(Dp left, float right)
    {
        return new Dp(left._value * right);
    }
    
    public static Dp operator *(int left, Dp right)
    {
        return new Dp(left * right._value);
    }
    
    public static Dp operator *(Dp left, int right)
    {
        return new Dp(left._value * right);
    }
    
    public static Dp operator /(Dp left, Dp right)
    {
        return new Dp(left._value / right._value);
    }
    
    public static Dp operator /(Dp left, float right)
    {
        return new Dp(left._value / right);
    }
    
    public static Dp operator /(Dp left, int right)
    {
        return new Dp(left._value / right);
    }

    public static bool operator ==(Dp lhs, Dp rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Dp lhs, Dp rhs)
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
    
    public static Percent operator *(float left, Percent right) {
        return new Percent(left * right._value);
    }
    
    public static Percent operator /(Percent left, float right) {
        return new Percent(left._value / right);
    }

    public static Percent operator +(Percent left, Percent right)
    {
        return new Percent(left._value + right._value);
    }

    public static Percent operator -(Percent left, Percent right)
    {
        return new Percent(left._value - right._value);
    }
    
    public static Percent operator -(Percent left)
    {
        return new Percent(-left._value);
    }

    public static Percent operator *(Percent left, Percent right)
    {
        return new Percent(left._value * right._value);
    }

    public static Percent operator *(Percent left, float right)
    {
        return new Percent(left._value * right);
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

    public static Dp Dp(this float value) => new Dp(value);


    public static Percent Percent(this int coordinate) => new(coordinate);

    public static Dp Dp(this int value) => new Dp(value);
}