using System.Runtime.CompilerServices;
using SharpExtensions;

namespace UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

public static class ParamUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Resolve(float first, float second)
    {
        return first >= 0 ? first : second;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Resolve(float first, float second, float third)
    {
        if (first >= 0)
            return first;
        return second >= 0 ? second : third;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Resolve(float first, float second, float third, float fourth)
    {
        if (first >= 0)
            return first;
        if (second >= 0)
            return second;
        return third >= 0 ? third : fourth;
    }
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Optional<T> Resolve<T>(Optional<T> first, Optional<T> second)
    {
        return first.HasValue ? first : second;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static  Optional<T> Resolve<T>(Optional<T> first, Optional<T> second, Optional<T> third)
    {
        if (first.HasValue)
            return first;
        return second.HasValue ? second : third;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static  Optional<T> Resolve<T>( Optional<T> first,  Optional<T> second,  Optional<T> third,  Optional<T> fourth)
    {
        if (first.HasValue)
            return first;
        if (second.HasValue)
            return second;
        return third.HasValue ? third : fourth;
    }
}