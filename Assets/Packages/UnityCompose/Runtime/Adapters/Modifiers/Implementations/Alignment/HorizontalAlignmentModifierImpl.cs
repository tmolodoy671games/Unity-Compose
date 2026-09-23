using Compose.Net;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class HorizontalAlignModifierImpl : BaseUnityModifier<HorizontalAlignModifierImpl>
{
    private readonly Alignment.Horizontal _align;

    public HorizontalAlignModifierImpl(Alignment.Horizontal align)
    {
        _align = align;
    }

    public override void Apply(VisualElement element)
    {
        switch (element.parent.NotNull().style.flexDirection.value)
        {
            case FlexDirection.Column:
            case FlexDirection.ColumnReverse:
                element.style.alignSelf = _align.ToAlign();
                break;
        }
    }

    public override void Revert(VisualElement element)
    {
        switch (element.parent.NotNull().style.flexDirection.value)
        {
            case FlexDirection.Column:
            case FlexDirection.ColumnReverse:
                element.style.alignSelf = StyleKeyword.Null;
                break;
        }
    }

    protected override bool Equals(HorizontalAlignModifierImpl other)
    {
        return _align == other._align;
    }
}
