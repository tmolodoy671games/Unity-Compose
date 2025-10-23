// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IExitTransition Exit(Func<IBoxScope, float, LayoutInfo, IModifier> transition)
    {
        return new CustomExitTransitionImpl(transition);
    }
}

internal class CustomExitTransitionImpl : IExitTransition
{
    private readonly Func<IBoxScope, float, LayoutInfo, IModifier> _transition;

    public CustomExitTransitionImpl(Func<IBoxScope, float, LayoutInfo, IModifier> transition)
    {
        _transition = transition;
    }

    public IModifier Get(IBoxScope scope, float progress, LayoutInfo parent)
    {
        return _transition(scope, progress, parent);
    }
}