using System;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeFunctions
{
    // Try switching to ThreadLocal for parallel Recomposition
    public static Composer CurrentComposer
    {
        get
        {
            var result = Composer.Current;
            return result ?? throw new InvalidOperationException("Not in composition context!");
        }
    }

    public static bool IsInPreview => !ApplicationUtils.IsPlaying;
    
    public static readonly IModifier Modifier = EmptyModifierImpl.Instance;

    public static ICompositionLocal<VisualElement> LocalVisualElement =
        CompositionLocalOf<VisualElement>(() => throw new ArgumentException("LocalVisualElement is not provided!"));
}