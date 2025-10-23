// ReSharper disable CheckNamespace

using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

namespace UnityCompose;

public static partial class EnterTransitionExtensions
{
    public static IEnterTransition Remap(
        this IEnterTransition enterTransition,
        float startOffset = 0f,
        float speed = 1f,
        float endOffset = 0f
    )
    {
        return new RemapEnterTransitionImpl(enterTransition, startOffset, speed, endOffset);
    }
}

internal class RemapEnterTransitionImpl : IEnterTransition
{
    private readonly IEnterTransition _original;
    private readonly float _startOffset;
    private readonly float _speed;
    private readonly float _endOffset;

    public RemapEnterTransitionImpl(IEnterTransition original, float startOffset, float speed, float endOffset)
    {
        _original = original;
        _startOffset = startOffset;
        _speed = speed;
        _endOffset = endOffset;
    }

    public IModifier Get(float progress, LayoutInfo parent)
    {
        return _original.Get(progress.Remap(_startOffset, _speed, _endOffset), parent);
    }
}