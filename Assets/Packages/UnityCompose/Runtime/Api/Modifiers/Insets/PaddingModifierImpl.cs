// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Padding(
        this IModifier modifier,
        LayoutLength all = default,
        LayoutLength horizontal = default,
        LayoutLength vertical = default,
        LayoutLength top = default,
        LayoutLength bottom = default,
        LayoutLength left = default,
        LayoutLength right = default,
        Optional<ComposeTransition> transition = default
    )
    {
        return modifier + new PaddingModifierImpl(
            top: ParamUtils.Resolve(top, vertical, all),
            bottom: ParamUtils.Resolve(bottom, vertical, all),
            left: ParamUtils.Resolve(left, horizontal, all),
            right: ParamUtils.Resolve(right, horizontal, all),
            transition: transition
        );
    }
}

internal class PaddingModifierImpl : BaseModifier<PaddingModifierImpl>
{
    private readonly LayoutLength _top;
    private readonly LayoutLength _bottom;
    private readonly LayoutLength _left;
    private readonly LayoutLength _right;
    private readonly Optional<ComposeTransition> _transition;

    public PaddingModifierImpl(
        LayoutLength top,
        LayoutLength bottom,
        LayoutLength left,
        LayoutLength right,
        Optional<ComposeTransition> transition
    )
    {
        _top = top;
        _bottom = bottom;
        _left = left;
        _right = right;
        _transition = transition;
    }

    public override void Apply(VisualElement element)
    {
        if (_top.HasValue)
        {
            element.style.paddingTop = _top;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "padding-left");
        }

        if (_bottom.HasValue)
        {
            element.style.paddingBottom = _bottom;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "padding-right");
        }

        if (_left.HasValue)
        {
            element.style.paddingLeft = _left;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "padding-left");
        }

        if (_right.HasValue)
        {
            element.style.paddingRight = _right;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "padding-right");
        }
    }

    public override void Revert(VisualElement element)
    {
        if (_top.HasValue)
        {
            element.style.paddingTop = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("padding-top");
        }

        if (_bottom.HasValue)
        {
            element.style.paddingBottom = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("padding-bottom");
        }

        if (_left.HasValue)
        {
            element.style.paddingLeft = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("padding-left");
        }

        if (_right.HasValue)
        {
            element.style.paddingRight = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("padding-right");
        }
    }

    protected override bool Equals(PaddingModifierImpl other)
    {
        return _top.Equals(other._top) &&
               _bottom.Equals(other._bottom) &&
               _left.Equals(other._left) &&
               _right.Equals(other._right);
    }
}