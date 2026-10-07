// ReSharper disable CheckNamespace

using System;
using System.Runtime.CompilerServices;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record DrawModifierImpl(
    Action<IDrawScope> OnDraw
) : DrawUnityModifier, IAppearanceModifier
{
    protected override Action<MeshGenerationContext> GenerateVisualContent { get; } = context =>
    {
        var scope = context.DrawScope();
        OnDraw(scope);
    };
}