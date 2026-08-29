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

    public static readonly ICompositionLocal<IFocusManager> LocalFocusManager =
        CompositionLocalOf<IFocusManager>(() => throw new InvalidOperationException("No LocalFocusManager provided!"));

    public static readonly ICompositionLocal<VisualElement> LocalVisualElement =
        CompositionLocalOf<VisualElement>(() => throw new ArgumentException("LocalVisualElement is not provided!"));

    public static IMutableInteractionSource MutableInteractionSource() => new MutableInteractionSourceImpl();
    
    public static void Repeat(int times, Action body)
    {
        for (var i = 0; i < times; i++)
            body();
    }
    
    public static void Repeat(int times, Action<int> body)
    {
        for (var i = 0; i < times; i++)
            body(i);
    }
}