// ReSharper disable CheckNamespace

using System;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record DrawBehindModifierImpl(
    Action<IDrawScope> OnDraw
) : DrawBehindUnityModifier, IAppearanceModifier
{
    protected override Action<MeshGenerationContext> GenerateVisualContent { get; } = it =>
    {
        var scope = it.DrawScope();
        OnDraw(scope);
    };
}