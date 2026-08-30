// ReSharper disable CheckNamespace

namespace UnityCompose;

public static partial class ComposeFunctions
{
    private static readonly ICompositionLocal<IsActiveEntry> LocalIsActive =
        CompositionLocalOf(() => new IsActiveEntry(true, null));

    [Composable] public static bool IsActive => LocalIsActive.Current.IsActiveRecursive();

    [Composable]
    public static void WithIsActive(bool active, ComposableContent content)
    {
        var isActiveInstance = LocalIsActive.Current;
        var newIsActiveInstance = Remember((isActiveInstance, active), () => new IsActiveEntry(
            IsActiveSelf: active,
            Parent: isActiveInstance
        ));
        CompositionLocalProvider(
            LocalIsActive.Provides(newIsActiveInstance),
            content
        );
    }
}

internal record IsActiveEntry(
    bool IsActiveSelf,
    IsActiveEntry? Parent
)
{
    public bool IsActiveRecursive() => IsActiveSelf && (Parent == null || Parent.IsActiveRecursive());
}