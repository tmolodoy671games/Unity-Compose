// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IExitTransition Hide() => HideExitTransitionImpl.Instance;
}

internal class HideExitTransitionImpl : IExitTransition
{
    public static readonly HideExitTransitionImpl Instance = new();
    
    private HideExitTransitionImpl() {}
    
    public IModifier Get(float progress, LayoutInfo parent)
    {
        return Modifier
            .Alpha(0f);
    }
}