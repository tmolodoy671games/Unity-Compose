// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

public readonly struct LayoutLength : IEquatable<LayoutLength>
{
    private readonly Length _value;

    private LayoutLength(Px px) : this()
    {
        _value = px.ToLength();
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

    public static implicit operator LayoutLength(Px px) => new(px);

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
            LengthUnit.Pixel => _value.value.Px().ToString(),
            LengthUnit.Percent => _value.value.Percent().ToString(),
            _ => throw new ArgumentOutOfRangeException()
        };
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
    
    public static Px operator -(Px left)
    {
        return new Px(-left._value);
    }

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
    
    public static Px operator *(float left, Px right)
    {
        return new Px(left * right._value);
    }
    
    public static Px operator *(Px left, float right)
    {
        return new Px(left._value * right);
    }
    
    public static Px operator *(int left, Px right)
    {
        return new Px(left * right._value);
    }
    
    public static Px operator *(Px left, int right)
    {
        return new Px(left._value * right);
    }
    
    public static Px operator /(Px left, Px right)
    {
        return new Px(left._value / right._value);
    }
    
    public static Px operator /(Px left, float right)
    {
        return new Px(left._value / right);
    }
    
    public static Px operator /(Px left, int right)
    {
        return new Px(left._value / right);
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

    public static Px Px(this float value) => new Px(value);


    public static Percent Percent(this int coordinate) => new(coordinate);

    public static Px Px(this int value) => new Px(value);
}