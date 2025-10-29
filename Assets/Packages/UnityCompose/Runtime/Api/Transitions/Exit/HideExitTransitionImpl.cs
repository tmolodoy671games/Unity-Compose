// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IExitTransition Hide(AnimationSpec animationSpec = default) =>
        new HideExitTransitionImpl(animationSpec);
}

internal class HideExitTransitionImpl : IExitTransition
{
    private readonly AnimationSpec _animationSpec;

    public HideExitTransitionImpl(AnimationSpec animationSpec)
    {
        _animationSpec = animationSpec.HasValue ? animationSpec : AnimationSpec.Default;
    }

    public float TotalDuration => 0f;

    public IModifier Get(float timeElapsed, LayoutInfo parent)
    {
        var resolvedProgress = _animationSpec.GetProgress(timeElapsed);
        if (resolvedProgress <= 0f)
            return Modifier;
        return Modifier
            .Alpha(0f);
    }
}