// ReSharper disable CheckNamespace

using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose;

public static partial class ExitTransitionExtensions
{
    public static IExitTransition Remap(
        this IExitTransition exitTransition,
        float startOffset = 0f,
        float speed = 1f,
        float endOffset = 0f
    )
    {
        return new RemapExitTransitionImpl(exitTransition, startOffset, speed, endOffset);
    }
}

internal class RemapExitTransitionImpl : IExitTransition
{
    private readonly IExitTransition _original;
    private readonly float _startOffset;
    private readonly float _speed;
    private readonly float _endOffset;

    public RemapExitTransitionImpl(IExitTransition original, float startOffset, float speed, float endOffset)
    {
        _original = original;
        _startOffset = startOffset;
        _speed = speed;
        _endOffset = endOffset;
    }

    public IModifier Get(IBoxScope scope, float progress, LayoutInfo parent)
    {
        return _original.Get(scope, progress.Remap(_startOffset, _speed, _endOffset), parent);
    }
}