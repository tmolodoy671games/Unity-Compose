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