// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using Compose.Net;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record BlurModifierImpl(float Strength) : UnityModifier, IAppearanceModifier
{
    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var filter = new FilterFunction(FilterFunctionType.Blur);
        filter.AddParameter(new FilterParameter(Strength));
        element.style.AddFilter(filter);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var filter = new FilterFunction(FilterFunctionType.Blur);
        filter.AddParameter(new FilterParameter(Strength));
        element.style.RemoveFilter(filter);
    }
}