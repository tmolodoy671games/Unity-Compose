// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;

namespace UnityCompose;

public record ContentTransform(
    IEnterTransition Enter,
    IExitTransition Exit
)
{
    public static readonly ContentTransform Instant = new(
        Enter: EmptyEnterTransitionImpl.Instance,
        Exit: HideExitTransitionImpl.Instance
    );

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ContentTransform operator +(ContentTransform first, ContentTransform second)
    {
        return new ContentTransform(
            Enter: first.Enter + second.Enter,
            Exit: first.Exit + second.Exit
        );
    }
}

public static class ContentTransformExtensions
{
    public static ContentTransform Remap(
        this ContentTransform contentTransform,
        float startOffset = 0f,
        float speed = 1f,
        float endOffset = 0f
    )
    {
        return new ContentTransform(
            Enter: contentTransform.Enter.Remap(startOffset, speed, endOffset),
            Exit: contentTransform.Exit.Remap(startOffset, speed, endOffset)
        );
    }
}