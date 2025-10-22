using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeStyleExtensions
{
    private class BorderBottomLeftRadiusImpl : ComposeStyle<BorderBottomLeftRadiusImpl>
    {
        private readonly StyleLength _borderBottomLeftRadius;
        private readonly ComposeTransition _transition;

        public BorderBottomLeftRadiusImpl(StyleLength borderBottomLeftRadius, ComposeTransition transition)
        {
            _borderBottomLeftRadius = borderBottomLeftRadius;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderBottomLeftRadius = _borderBottomLeftRadius;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-bottom-left-radius");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderBottomLeftRadius);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderBottomLeftRadius = _borderBottomLeftRadius;
        }

        protected override bool Compare(BorderBottomLeftRadiusImpl other)
        {
            return other._borderBottomLeftRadius == _borderBottomLeftRadius &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderBottomRightRadiusImpl : ComposeStyle<BorderBottomRightRadiusImpl>
    {
        private readonly StyleLength _borderBottomRightRadius;
        private readonly ComposeTransition _transition;

        public BorderBottomRightRadiusImpl(StyleLength borderBottomRightRadius, ComposeTransition transition)
        {
            _borderBottomRightRadius = borderBottomRightRadius;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderBottomRightRadius = _borderBottomRightRadius;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-bottom-right-radius");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderBottomRightRadius);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderBottomRightRadius = _borderBottomRightRadius;
        }

        protected override bool Compare(BorderBottomRightRadiusImpl other)
        {
            return _borderBottomRightRadius == other._borderBottomRightRadius &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderTopLeftRadiusImpl : ComposeStyle<BorderTopLeftRadiusImpl>
    {
        private readonly StyleLength _borderTopLeftRadius;
        private readonly ComposeTransition _transition;

        public BorderTopLeftRadiusImpl(StyleLength borderTopLeftRadius, ComposeTransition transition)
        {
            _borderTopLeftRadius = borderTopLeftRadius;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderTopLeftRadius = _borderTopLeftRadius;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-top-left-radius");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderTopLeftRadius);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderTopLeftRadius = StyleKeyword.Null;
        }

        protected override bool Compare(BorderTopLeftRadiusImpl other)
        {
            return _borderTopLeftRadius == other._borderTopLeftRadius &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderTopRightRadiusImpl : ComposeStyle<BorderTopRightRadiusImpl>
    {
        private readonly StyleLength _borderTopRightRadius;
        private readonly ComposeTransition _transition;

        public BorderTopRightRadiusImpl(StyleLength borderTopRightRadius, ComposeTransition transition)
        {
            _borderTopRightRadius = borderTopRightRadius;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderTopRightRadius = _borderTopRightRadius;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-top-right-radius");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderTopRightRadius);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderTopRightRadius = StyleKeyword.Null;
        }

        protected override bool Compare(BorderTopRightRadiusImpl other)
        {
            return _borderTopRightRadius == other._borderTopRightRadius &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderTopWidthImpl : ComposeStyle<BorderTopWidthImpl>
    {
        private readonly StyleFloat _borderTopWidth;
        private readonly ComposeTransition _transition;

        public BorderTopWidthImpl(StyleFloat borderTopWidth, ComposeTransition transition)
        {
            _borderTopWidth = borderTopWidth;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderTopWidth = _borderTopWidth;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-top-width");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderTopWidth);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderTopWidth = StyleKeyword.Null;
        }

        protected override bool Compare(BorderTopWidthImpl other)
        {
            return _borderTopWidth == other._borderTopWidth && Equals(_transition, other._transition);
        }
    }

    private class BorderBottomWidthImpl : ComposeStyle<BorderBottomWidthImpl>
    {
        private readonly StyleFloat _borderBottomWidth;
        private readonly ComposeTransition _transition;

        public BorderBottomWidthImpl(StyleFloat borderBottomWidth, ComposeTransition transition)
        {
            _borderBottomWidth = borderBottomWidth;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderBottomWidth = _borderBottomWidth;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-bottom-width");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderBottomWidth);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderBottomWidth = StyleKeyword.Null;
        }

        protected override bool Compare(BorderBottomWidthImpl other)
        {
            return _borderBottomWidth == other._borderBottomWidth &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderLeftWidthImpl : ComposeStyle<BorderLeftWidthImpl>
    {
        private readonly StyleFloat _borderLeftWidth;
        private readonly ComposeTransition _transition;

        public BorderLeftWidthImpl(StyleFloat borderLeftWidth, ComposeTransition transition)
        {
            _borderLeftWidth = borderLeftWidth;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderLeftWidth = _borderLeftWidth;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-left-width");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderLeftWidth);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderLeftWidth = StyleKeyword.Null;
        }

        protected override bool Compare(BorderLeftWidthImpl other)
        {
            return _borderLeftWidth == other._borderLeftWidth &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderRightWidthImpl : ComposeStyle<BorderRightWidthImpl>
    {
        private readonly StyleFloat _borderRightWidth;
        private readonly ComposeTransition _transition;

        public BorderRightWidthImpl(StyleFloat borderRightWidth, ComposeTransition transition)
        {
            _borderRightWidth = borderRightWidth;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderRightWidth = _borderRightWidth;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-right-width");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderRightWidth);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderRightWidth = StyleKeyword.Null;
        }

        protected override bool Compare(BorderRightWidthImpl other)
        {
            return _borderRightWidth == other._borderRightWidth &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderTopColorImpl : ComposeStyle<BorderTopColorImpl>
    {
        private readonly StyleColor _borderTopColor;
        private readonly ComposeTransition _transition;

        public BorderTopColorImpl(StyleColor borderTopColor, ComposeTransition transition)
        {
            _borderTopColor = borderTopColor;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderTopColor = _borderTopColor;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-top-color");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderTopColor);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderTopColor = StyleKeyword.Null;
        }

        protected override bool Compare(BorderTopColorImpl other)
        {
            return _borderTopColor == other._borderTopColor &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderBottomColorImpl : ComposeStyle<BorderBottomColorImpl>
    {
        private readonly StyleColor _borderBottomColor;
        private readonly ComposeTransition _transition;

        public BorderBottomColorImpl(StyleColor borderBottomColor, ComposeTransition transition)
        {
            _borderBottomColor = borderBottomColor;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderBottomColor = _borderBottomColor;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-bottom-color");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderBottomColor);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderBottomColor = StyleKeyword.Null;
        }

        protected override bool Compare(BorderBottomColorImpl other)
        {
            return _borderBottomColor == other._borderBottomColor &&
                   Equals(_transition, other._transition);
            ;
        }
    }

    private class BorderLeftColorImpl : ComposeStyle<BorderLeftColorImpl>
    {
        private readonly StyleColor _borderLeftColor;
        private readonly ComposeTransition _transition;

        public BorderLeftColorImpl(StyleColor borderLeftColor, ComposeTransition transition)
        {
            _borderLeftColor = borderLeftColor;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderLeftColor = _borderLeftColor;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-left-color");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderLeftColor);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderLeftColor = StyleKeyword.Null;
        }

        protected override bool Compare(BorderLeftColorImpl other)
        {
            return _borderLeftColor == other._borderLeftColor &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderRightColorImpl : ComposeStyle<BorderRightColorImpl>
    {
        private readonly StyleColor _borderRightColor;
        private readonly ComposeTransition _transition;

        public BorderRightColorImpl(StyleColor borderRightColor, ComposeTransition transition)
        {
            _borderRightColor = borderRightColor;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.borderRightColor = _borderRightColor;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "border-right-color");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.BorderRightColor);
        }

        public override void Revert(VisualElement element)
        {
            element.style.borderRightColor = StyleKeyword.Null;
        }

        protected override bool Compare(BorderRightColorImpl other)
        {
            return _borderRightColor == other._borderRightColor &&
                   Equals(_transition, other._transition);
            ;
        }
    }

    public static ComposeStyle BorderBottomLeftRadius(
        this ComposeStyle style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderBottomLeftRadiusImpl(radius, transition));
    }

    public static ComposeStyle BorderBottomRightRadius(
        this ComposeStyle style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderBottomRightRadiusImpl(radius, transition));
    }

    public static ComposeStyle BorderTopLeftRadius(
        this ComposeStyle style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderTopLeftRadiusImpl(radius, transition));
    }

    public static ComposeStyle BorderTopRightRadius(
        this ComposeStyle style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderTopRightRadiusImpl(radius, transition));
    }

    public static ComposeStyle BorderRadius(
        this ComposeStyle style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderBottomLeftRadiusImpl(radius, transition))
            .Then(new BorderBottomRightRadiusImpl(radius, transition))
            .Then(new BorderTopLeftRadiusImpl(radius, transition))
            .Then(new BorderTopRightRadiusImpl(radius, transition));
    }

    public static ComposeStyle BorderTopRadius(
        this ComposeStyle style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderTopLeftRadiusImpl(radius, transition))
            .Then(new BorderTopRightRadiusImpl(radius, transition));
    }

    public static ComposeStyle BorderBottomRadius(
        this ComposeStyle style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderBottomLeftRadiusImpl(radius, transition))
            .Then(new BorderBottomRightRadiusImpl(radius, transition));
    }

    public static ComposeStyle BorderLeftRadius(
        this ComposeStyle style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderTopLeftRadiusImpl(radius, transition))
            .Then(new BorderBottomLeftRadiusImpl(radius, transition));
    }

    public static ComposeStyle BorderRightRadius(
        this ComposeStyle style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderTopRightRadiusImpl(radius, transition))
            .Then(new BorderBottomRightRadiusImpl(radius, transition));
    }

    public static ComposeStyle BorderTopWidth(
        this ComposeStyle style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderTopWidthImpl(width, transition));
    }

    public static ComposeStyle BorderBottomWidth(
        this ComposeStyle style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderBottomWidthImpl(width, transition));
    }

    public static ComposeStyle BorderLeftWidth(
        this ComposeStyle style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderLeftWidthImpl(width, transition));
    }

    public static ComposeStyle BorderRightWidth(
        this ComposeStyle style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderRightWidthImpl(width, transition));
    }

    public static ComposeStyle BorderWidth(
        this ComposeStyle style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderLeftWidthImpl(width, transition))
            .Then(new BorderRightWidthImpl(width, transition))
            .Then(new BorderBottomWidthImpl(width, transition))
            .Then(new BorderTopWidthImpl(width, transition));
    }

    public static ComposeStyle BorderWidth(
        this ComposeStyle style,
        StyleFloat horizontalWidth,
        StyleFloat verticalWidth,
        ComposeTransition transition = default
    )
    {
        return style
            .BorderHorizontalWidth(horizontalWidth, transition)
            .BorderVerticalWidth(verticalWidth, transition);
    }

    public static ComposeStyle BorderHorizontalWidth(
        this ComposeStyle style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderLeftWidthImpl(width, transition))
            .Then(new BorderRightWidthImpl(width, transition));
    }

    public static ComposeStyle BorderVerticalWidth(
        this ComposeStyle style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderTopWidthImpl(width, transition))
            .Then(new BorderBottomWidthImpl(width, transition));
    }

    public static ComposeStyle BorderTopColor(
        this ComposeStyle style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderTopColorImpl(color, transition));
    }

    public static ComposeStyle BorderBottomColor(
        this ComposeStyle style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderBottomColorImpl(color, transition));
    }

    public static ComposeStyle BorderLeftColor(
        this ComposeStyle style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderLeftColorImpl(color, transition));
    }

    public static ComposeStyle BorderRightColor(
        this ComposeStyle style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderRightColorImpl(color, transition));
    }

    public static ComposeStyle BorderColor(
        this ComposeStyle style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderLeftColorImpl(color, transition))
            .Then(new BorderRightColorImpl(color, transition))
            .Then(new BorderBottomColorImpl(color, transition))
            .Then(new BorderTopColorImpl(color, transition));
    }

    public static ComposeStyle BorderColor(
        this ComposeStyle style,
        StyleColor horizontalColor,
        StyleColor verticalColor,
        ComposeTransition transition = default
    )
    {
        return style
            .BorderHorizontalColor(horizontalColor, transition)
            .BorderVerticalColor(verticalColor, transition);
    }

    public static ComposeStyle BorderHorizontalColor(
        this ComposeStyle style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderLeftColorImpl(color, transition))
            .Then(new BorderRightColorImpl(color, transition));
    }

    public static ComposeStyle BorderVerticalColor(
        this ComposeStyle style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderTopColorImpl(color, transition))
            .Then(new BorderBottomColorImpl(color, transition));
    }
}