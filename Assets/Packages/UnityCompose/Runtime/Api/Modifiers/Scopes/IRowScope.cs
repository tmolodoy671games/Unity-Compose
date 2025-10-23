// ReSharper disable CheckNamespace

using SharpExtensions;
using StableCollections;
using UnityEngine.UIElements;

namespace UnityCompose;

public interface IRowScope
{
    IModifier Align(HorizontalAlign align);

    IModifier FillMaxWidth();
    IModifier Weight(float fraction);
}

internal class RowScopeImpl : IRowScope
{
    public IModifier Align(HorizontalAlign align)
    {
        return Modifier + new HorizontalAlignModifierImpl(align);
    }

    public IModifier FillMaxWidth()
    {
        return Modifier + new WeightModifierImpl(1);
    }

    public IModifier Weight(float fraction)
    {
        return Modifier + new WeightModifierImpl(fraction);
    }
}