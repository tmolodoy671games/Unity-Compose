// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using Compose.Net;
using SharpExtensions;
using StableCollections;
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
        
        if (element.style.filter.value == null)
            element.style.filter = new List<FilterFunction>();
        element.style.filter.value.Add(filter);
        element.style.filter = element.style.filter.value.ToList();
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        var filter = new FilterFunction(FilterFunctionType.Blur);
        filter.AddParameter(new FilterParameter(Strength));
        
        if (element.style.filter.value == null)
            element.style.filter = new List<FilterFunction>();
        element.style.filter.value.Remove(filter);
        element.style.filter = element.style.filter.value.ToList();
    }
}