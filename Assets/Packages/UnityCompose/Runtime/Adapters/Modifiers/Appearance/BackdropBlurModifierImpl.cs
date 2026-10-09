// ReSharper disable CheckNamespace

using Compose.Net;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

namespace UnityCompose;

internal record BackdropBlurModifierImpl(Dp Radius) : UnityModifier, IAppearanceModifier
{
    protected override void Apply(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (Radius.Value <= 0f)
            return;
        var filter = new FilterFunction(FilterFunctionType.Blur);
        filter.AddParameter(new FilterParameter(Radius.Value));
        element.style.AddFilter(filter);
    }

    protected override void Revert(
        UnityReusableComposeNode node,
        VisualElement element,
        IStableList<IModifier> newModifiers
    )
    {
        if (Radius.Value <= 0f)
            return;
        var filter = new FilterFunction(FilterFunctionType.Blur);
        filter.AddParameter(new FilterParameter(Radius.Value));
        element.style.RemoveFilter(filter);
    }
}