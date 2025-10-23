// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnterTransition Enter(Func<float, LayoutInfo, IModifier> transition)
    {
        return new CustomEnterTransitionImpl(transition);
    }
}

internal class CustomEnterTransitionImpl : IEnterTransition
{
    private readonly Func<float, LayoutInfo, IModifier> _transition;

    public CustomEnterTransitionImpl(Func<float, LayoutInfo, IModifier> transition)
    {
        _transition = transition;
    }

    public IModifier Get(float progress, LayoutInfo parent)
    {
        return _transition(progress, parent);
    }
}