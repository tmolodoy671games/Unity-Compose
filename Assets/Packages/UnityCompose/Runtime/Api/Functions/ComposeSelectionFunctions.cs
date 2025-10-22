using StableCollections;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    public static IComposeSelection<T> RememberSelection<T>(
        IImmutableStableList<T> list,
        T initialValue,
        bool canBeCycled = false
    )
    {
        return Remember(
            key: (list, initialValue, canBeCycled),
            defaultValueFactory: () => new ComposeSelectionImpl<T>(list, initialValue, canBeCycled)
        );
    }

    public static IComposeSelection2D<T> RememberSelection<T>(
        IImmutableStableArray2D<T> list,
        T initialValue,
        bool canBeCycled = false
    )
    {
        return Remember(
            key: (list, initialValue, canBeCycled),
            defaultValueFactory: () => new ComposeSelection2DImpl<T>(list, initialValue, canBeCycled)
        );
    }
}