using System;
using System.Diagnostics.CodeAnalysis;
using UnityCompose.Packages.UnityCompose.Runtime.Api.Models;
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
    
    public static RoundedCornerShape RoundedCornerShape(
        LayoutLength size
    )
    {
        return new RoundedCornerShape(
            TopLeft: size,
            TopRight: size,
            BottomLeft: size,
            BottomRight: size
        );
    }

    [SuppressMessage("ReSharper", "MethodOverloadWithOptionalParameter")]
    public static RoundedCornerShape RoundedCornerShape(
        LayoutLength topLeft = default,
        LayoutLength topRight = default,
        LayoutLength bottomLeft = default,
        LayoutLength bottomRight = default
    )
    {
        return new RoundedCornerShape(
            TopLeft: topLeft,
            TopRight: topRight,
            BottomLeft: bottomLeft,
            BottomRight: bottomRight
        );
    }
    
    public static PaddingValues PaddingValues(
        Dp all
    )
    {
        return new PaddingValues(
            Top: all,
            Bottom: all,
            Left: all,
            Right: all
        );
    }

    [SuppressMessage("ReSharper", "MethodOverloadWithOptionalParameter")]
    public static PaddingValues PaddingValues(
        Dp horizontal = default,
        Dp vertical = default
    )
    {
        return new PaddingValues(
            Top: vertical,
            Bottom: vertical,
            Left: horizontal,
            Right: horizontal
        );
    }

    [SuppressMessage("ReSharper", "MethodOverloadWithOptionalParameter")]
    public static PaddingValues PaddingValues(
        Dp top = default,
        Dp bottom = default,
        Dp left = default,
        Dp right = default
    )
    {
        return new PaddingValues(
            Top: top,
            Bottom: bottom,
            Left: left,
            Right: right
        );
    }
}