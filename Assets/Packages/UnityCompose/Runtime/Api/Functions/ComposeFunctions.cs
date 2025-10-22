using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    // Try switching to ThreadLocal for parallel Recomposition
    public static Composer CurrentComposer => Composer.Instance;
    public static bool IsInPreview => !ApplicationUtils.IsPlaying;
}