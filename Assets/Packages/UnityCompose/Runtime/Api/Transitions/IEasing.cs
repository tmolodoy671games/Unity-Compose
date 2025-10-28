// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static readonly IEasing LinearEasing = AnimationCurveEasing(AnimationCurve.Linear(0, 0, 1, 1));
    public static readonly IEasing EaseInOutEasing = AnimationCurveEasing(AnimationCurve.EaseInOut(0, 0, 1, 1));
    private static IEasing EaseOutEasing = Easing(it => 1 - Mathf.Pow(1 - it, 3));
    private static IEasing EaseInEasing = Easing(it => Mathf.Pow(it, 3));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEasing Easing(Func<float, float> easing) => new CustomEasingImpl(easing);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEasing AnimationCurveEasing(AnimationCurve curve) => new AnimationCurveEasingImpl(curve);
}

public interface IEasing
{
    float Transform(float fraction);
}

internal class AnimationCurveEasingImpl : IEasing
{
    private readonly AnimationCurve _curve;

    public AnimationCurveEasingImpl(AnimationCurve curve)
    {
        _curve = curve;
    }

    public float Transform(float fraction)
    {
        return _curve.Evaluate(fraction);
    }

    protected bool Equals(AnimationCurveEasingImpl other)
    {
        return _curve.Equals(other._curve);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((AnimationCurveEasingImpl)obj);
    }

    public override int GetHashCode()
    {
        return _curve.GetHashCode();
    }
}

internal class CustomEasingImpl : IEasing
{
    private readonly Func<float, float> _easing;

    public CustomEasingImpl(Func<float, float> easing)
    {
        _easing = easing;
    }

    public float Transform(float fraction)
    {
        return _easing(fraction);
    }

    protected bool Equals(CustomEasingImpl other)
    {
        return _easing.Equals(other._easing);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((CustomEasingImpl)obj);
    }

    public override int GetHashCode()
    {
        return _easing.GetHashCode();
    }
}