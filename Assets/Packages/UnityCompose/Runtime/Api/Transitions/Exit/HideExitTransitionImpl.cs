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

    public float TotalDuration => 0f;
    
    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        return Modifier
            .Alpha(0f);
    }
}