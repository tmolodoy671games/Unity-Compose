// ReSharper disable CheckNamespace

using SharpExtensions;

namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IModifier Modifier => EmptyModifierImpl.Instance;

    public static IComposer CurrentComposer
    {
        [Composable, Compiled] get => ComposerImpl.Current.NotNull();
    }
}