using System.Runtime.CompilerServices;
using SharpExtensions;
using UnityEngine.UIElements;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

public static class ParamUtils
{
    public static float Resolve(float first, float second)
    {
        return first >= 0 ? first : second;
    }

    public static float Resolve(float first, float second, float third)
    {
        if (first >= 0)
            return first;
        return second >= 0 ? second : third;
    }

    public static float Resolve(float first, float second, float third, float fourth)
    {
        if (first >= 0)
            return first;
        if (second >= 0)
            return second;
        return third >= 0 ? third : fourth;
    }

    public static Optional<T> Resolve<T>(Optional<T> first, Optional<T> second)
    {
        return first.HasValue ? first : second;
    }

    public static Optional<T> Resolve<T>(Optional<T> first, Optional<T> second, Optional<T> third)
    {
        if (first.HasValue)
            return first;
        return second.HasValue ? second : third;
    }

    public static Optional<T> Resolve<T>(Optional<T> first, Optional<T> second, Optional<T> third, Optional<T> fourth)
    {
        if (first.HasValue)
            return first;
        if (second.HasValue)
            return second;
        return third.HasValue ? third : fourth;
    }

    public static LayoutLength Resolve(LayoutLength first, LayoutLength second)
    {
        return first.HasValue ? first : second;
    }

    public static LayoutLength Resolve(LayoutLength first, LayoutLength second, LayoutLength third)
    {
        if (first.HasValue)
            return first;
        return second.HasValue ? second : third;
    }

    public static LayoutLength Resolve(LayoutLength first, LayoutLength second, LayoutLength third,
        LayoutLength fourth)
    {
        if (first.HasValue)
            return first;
        if (second.HasValue)
            return second;
        return third.HasValue ? third : fourth;
    }
}