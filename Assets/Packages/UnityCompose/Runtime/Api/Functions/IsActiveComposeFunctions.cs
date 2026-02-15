// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ComposeFunctions
{
    internal static readonly ICompositionLocal<IsActiveEntry> LocalIsActive =
        CompositionLocalOf(() => new IsActiveEntry(true, null));

    [Composable]
    public static bool IsActive => LocalIsActive.Current.IsActiveRecursive();
}

internal record IsActiveEntry(
    bool IsActiveSelf,
    IsActiveEntry? Parent
)
{
    public bool IsActiveRecursive() => IsActiveSelf && (Parent == null || Parent.IsActiveRecursive());
}