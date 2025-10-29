// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using UnityEngine;

namespace UnityCompose;

public readonly record struct ContentTransform(
    IEnterTransition Enter,
    IExitTransition Exit
)
{
    public static readonly ContentTransform Instant = new(
        Enter: IEnterTransition.Empty(),
        Exit: IExitTransition.Empty()
    );

    public float TotalDuration => Mathf.Max(Enter.TotalDuration, Exit.TotalDuration);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ContentTransform operator +(ContentTransform first, ContentTransform second)
    {
        return new ContentTransform(
            Enter: first.Enter + second.Enter,
            Exit: first.Exit + second.Exit
        );
    }
}

public static partial class EnterTransitionExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ContentTransform TogetherWith(this IEnterTransition enter, IExitTransition exit)
    {
        return new ContentTransform(enter, exit);
    }
}