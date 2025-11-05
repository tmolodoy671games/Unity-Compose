// ReSharper disable CheckNamespace

using System.Runtime.CompilerServices;
using SharpExtensions;
using StableCollections;
using UnityCompose.Packages.UnityCompose.Runtime.Impl.Utils;
using UnityEngine.UIElements;

namespace UnityCompose;

public static partial class ModifierExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IModifier Margin(
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
        return modifier + new MarginModifierImpl(
            top: ParamUtils.Resolve(top, vertical, all),
            bottom: ParamUtils.Resolve(bottom, vertical, all),
            left: ParamUtils.Resolve(left, horizontal, all),
            right: ParamUtils.Resolve(right, horizontal, all),
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

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        if (_top.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.MarginTop);
        if (_bottom.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.MarginBottom);
        if (_left.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.MarginLeft);
        if (_right.HasValue)
            modifiedProperties.Add(ComposeModifiedProperty.MarginRight);
    }

    public override void Revert(VisualElement element)
    {
        if (_top.HasValue)
            element.style.marginTop = StyleKeyword.Null;
        if (_bottom.HasValue)
            element.style.marginBottom = StyleKeyword.Null;
        if (_left.HasValue)
            element.style.marginLeft = StyleKeyword.Null;
        if (_right.HasValue)
            element.style.marginRight = StyleKeyword.Null;
    }

    protected override bool Equals(MarginModifierImpl other)
    {
        return _top.Equals(other._top) &&
               _bottom.Equals(other._bottom) &&
               _left.Equals(other._left) &&
               _right.Equals(other._right);
    }
}