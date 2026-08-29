// ReSharper disable CheckNamespace

using System;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose;

public readonly struct Sp : IEquatable<Sp>
{
    internal readonly float Value;

    public Sp(float value)
    {
        Value = value;
    }

    public bool Equals(Sp other)
    {
        return Value.AlmostEquals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Sp other && Equals(other);
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString()
    {
        return $"{Value}px";
    }
    
    public static Sp operator -(Sp left)
    {
        return new Sp(-left.Value);
    }

    public static Sp operator +(Sp left, Sp right)
    {
        return new Sp(left.Value + right.Value);
    }

    public static Sp operator -(Sp left, Sp right)
    {
        return new Sp(left.Value - right.Value);
    }

    public static Sp operator *(Sp left, Sp right)
    {
        return new Sp(left.Value * right.Value);
    }
    
    public static Sp operator *(float left, Sp right)
    {
        return new Sp(left * right.Value);
    }
    
    public static Sp operator *(Sp left, float right)
    {
        return new Sp(left.Value * right);
    }
    
    public static Sp operator *(int left, Sp right)
    {
        return new Sp(left * right.Value);
    }
    
    public static Sp operator *(Sp left, int right)
    {
        return new Sp(left.Value * right);
    }
    
    public static Sp operator /(Sp left, Sp right)
    {
        return new Sp(left.Value / right.Value);
    }
    
    public static Sp operator /(Sp left, float right)
    {
        return new Sp(left.Value / right);
    }
    
    public static Sp operator /(Sp left, int right)
    {
        return new Sp(left.Value / right);
    }

    public static bool operator ==(Sp lhs, Sp rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(Sp lhs, Sp rhs)
    {
        return !(lhs == rhs);
    }
}

public static partial class SpExtensions
{
    [Composable]
    public static float Resolve(this Sp sp)
    {
        return sp.Value * LocalTextScale.Current;
    }
}

public static partial class FloatExtensions
{

    public static Sp Sp(this float value) => new Sp(value);

    public static Sp Sp(this int value) => new Sp(value);
}