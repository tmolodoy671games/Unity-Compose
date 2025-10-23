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
        float all = -1,
        float horizontal = -1,
        float vertical = -1,
        float top = -1,
        float bottom = -1,
        float left = -1,
        float right = -1,
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
    private readonly float _top;
    private readonly float _bottom;
    private readonly float _left;
    private readonly float _right;
    private readonly Optional<ComposeTransition> _transition;

    public MarginModifierImpl(
        float top,
        float bottom,
        float left,
        float right,
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
        if (_top >= 0)
        {
            element.style.marginTop = _top;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "margin-left");
        }

        if (_bottom >= 0)
        {
            element.style.marginBottom = _bottom;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "margin-right");
        }

        if (_left >= 0)
        {
            element.style.marginLeft = _left;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "margin-left");
        }

        if (_right >= 0)
        {
            element.style.marginRight = _right;
            if (_transition.HasValue)
                element.AddTransition(_transition.Value, "margin-right");
        }
    }

    public override void Apply(IMutableStableCollection<ComposeModifiedProperty> modifiedProperties)
    {
        if (_top >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.MarginTop);
        if (_bottom >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.MarginBottom);
        if (_left >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.MarginLeft);
        if (_right >= 0)
            modifiedProperties.Add(ComposeModifiedProperty.MarginRight);
    }

    public override void Revert(VisualElement element)
    {
        if (_top >= 0)
            element.style.marginTop = _top;
        if (_bottom >= 0)
            element.style.marginBottom = _bottom;
        if (_left >= 0)
            element.style.marginLeft = _left;
        if (_right >= 0)
            element.style.marginRight = _right;
    }

    protected override bool Equals(MarginModifierImpl other)
    {
        return _top.AlmostEquals(other._top) &&
               _bottom.AlmostEquals(other._bottom) &&
               _left.AlmostEquals(other._left) &&
               _right.AlmostEquals(other._right);
    }
}