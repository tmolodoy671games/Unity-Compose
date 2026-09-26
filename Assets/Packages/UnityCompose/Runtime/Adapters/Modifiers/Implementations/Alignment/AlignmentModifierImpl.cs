using Compose.Net;
using SharpExtensions;
using UnityCompose.Packages.UnityCompose.Runtime.Adapters.Extensions;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace

namespace UnityCompose;

internal class AlignModifierImpl : BaseUnityModifier<AlignModifierImpl>
{
    private readonly Alignment _align;

    public AlignModifierImpl(Alignment align)
    {
        _align = align;
    }

    public override void Apply(VisualElement element)
    {
        switch (element.parent.NotNull().style.flexDirection.value)
        {
            case FlexDirection.Row:
            case FlexDirection.RowReverse:
                element.style.alignSelf = _align.ToAlign();
                break;
        }
    }

    public override void Revert(VisualElement element)
    {
        switch (element.parent.NotNull().style.flexDirection.value)
        {
            case FlexDirection.Row:
            case FlexDirection.RowReverse:
                element.style.alignSelf = StyleKeyword.Null;
                break;
        }
    }

    protected override bool Equals(AlignModifierImpl other)
    {
        return _align == other._align;
    }
}