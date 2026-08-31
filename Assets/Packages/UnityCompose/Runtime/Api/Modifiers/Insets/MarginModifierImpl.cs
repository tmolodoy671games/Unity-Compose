// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    public static IModifier Margin(
        this IModifier modifier,
        LayoutLength all,
        Optional<ComposeTransition> transition = default
    )
    {
        return modifier + new MarginModifierImpl(
            top: all,
            bottom: all,
            left: all,
            right: all,
            transition: transition
        );
    }
    
    public static IModifier Margin(
        this IModifier modifier,
        LayoutLength horizontal = default,
        LayoutLength vertical = default,
        Optional<ComposeTransition> transition = default
    )
    {
        return modifier + new MarginModifierImpl(
            top: vertical,
            bottom: vertical,
            left: horizontal,
            right: horizontal,
            transition: transition
        );
    }
    
    public static IModifier Margin(
        this IModifier modifier,
        LayoutLength top = default,
        LayoutLength bottom = default,
        LayoutLength left = default,
        LayoutLength right = default,
        Optional<ComposeTransition> transition = default
    )
    {
        return modifier + new MarginModifierImpl(
            top: top,
            bottom: bottom,
            left: left,
            right: right,
            transition: transition
        );
    }
}

internal class MarginModifierImpl : BaseModifier<MarginModifierImpl>
{
    private readonly LayoutLength _top;
    private readonly LayoutLength _bottom;
    private readonly LayoutLength _left;
    private readonly LayoutLength _right;
    private readonly Optional<ComposeTransition> _transition;

    public MarginModifierImpl(
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
            element.style.marginTop = _top.ToLength();
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "margin-left");
        }

        if (_bottom.HasValue)
        {
            element.style.marginBottom = _bottom.ToLength();
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "margin-right");
        }

        if (_left.HasValue)
        {
            element.style.marginLeft = _left.ToLength();
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "margin-left");
        }

        if (_right.HasValue)
        {
            element.style.marginRight = _right.ToLength();
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "margin-right");
        }
    }

    public override void Revert(VisualElement element)
    {
        if (_top.HasValue)
        {
            element.style.marginTop = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("margin-top");
        }

        if (_bottom.HasValue)
        {
            element.style.marginBottom = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("margin-bottom");
        }

        if (_left.HasValue)
        {
            element.style.marginLeft = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("margin-left");
        }

        if (_right.HasValue)
        {
            element.style.marginRight = StyleKeyword.Null;
            if (_transition.HasValue)
                element.RemoveTransition("margin-right");
        }
    }

    protected override bool Equals(MarginModifierImpl other)
    {
        return _top.Equals(other._top) &&
               _bottom.Equals(other._bottom) &&
               _left.Equals(other._left) &&
               _right.Equals(other._right);
    }
}