// ReSharper disable CheckNamespace

using System;
using System.Drawing;
using Compose.Net;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions.Values;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record BackgroundColorShapeModifierImpl(
    Color BackgroundColor,
    Shape Shape
) : DrawUnityModifier, IAppearanceModifier
{
    protected override Action<MeshGenerationContext> GenerateVisualContent { get; } = it =>
    {
        var drawScope = it.DrawScope();
        drawScope.DrawShape(
            color: BackgroundColor,
            shape: Shape
        );
    };
}

internal record BackgroundColorModifierImpl(
    Color BackgroundColor
) : UnityModifier, IAppearanceModifier
{
    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        element.style.backgroundColor = BackgroundColor.ToUnityColor();
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var style = element.style;
        style.backgroundColor = style.backgroundColor.CompareAndSetNull(BackgroundColor.ToUnityColor());
    }
}