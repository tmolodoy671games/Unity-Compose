using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeStyleExtensions
{
    private class BorderBottomLeftRadiusImpl : BaseModifier<BorderBottomLeftRadiusImpl>
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

        protected override bool Equals(BorderBottomLeftRadiusImpl other)
        {
            return other._borderBottomLeftRadius == _borderBottomLeftRadius &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderBottomRightRadiusImpl : BaseModifier<BorderBottomRightRadiusImpl>
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

        protected override bool Equals(BorderBottomRightRadiusImpl other)
        {
            return _borderBottomRightRadius == other._borderBottomRightRadius &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderTopLeftRadiusImpl : BaseModifier<BorderTopLeftRadiusImpl>
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

        protected override bool Equals(BorderTopLeftRadiusImpl other)
        {
            return _borderTopLeftRadius == other._borderTopLeftRadius &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderTopRightRadiusImpl : BaseModifier<BorderTopRightRadiusImpl>
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

        protected override bool Equals(BorderTopRightRadiusImpl other)
        {
            return _borderTopRightRadius == other._borderTopRightRadius &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderTopWidthImpl : BaseModifier<BorderTopWidthImpl>
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

        protected override bool Equals(BorderTopWidthImpl other)
        {
            return _borderTopWidth == other._borderTopWidth && Equals(_transition, other._transition);
        }
    }

    private class BorderBottomWidthImpl : BaseModifier<BorderBottomWidthImpl>
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

        protected override bool Equals(BorderBottomWidthImpl other)
        {
            return _borderBottomWidth == other._borderBottomWidth &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderLeftWidthImpl : BaseModifier<BorderLeftWidthImpl>
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

        protected override bool Equals(BorderLeftWidthImpl other)
        {
            return _borderLeftWidth == other._borderLeftWidth &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderRightWidthImpl : BaseModifier<BorderRightWidthImpl>
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

        protected override bool Equals(BorderRightWidthImpl other)
        {
            return _borderRightWidth == other._borderRightWidth &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderTopColorImpl : BaseModifier<BorderTopColorImpl>
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

        protected override bool Equals(BorderTopColorImpl other)
        {
            return _borderTopColor == other._borderTopColor &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderBottomColorImpl : BaseModifier<BorderBottomColorImpl>
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

        protected override bool Equals(BorderBottomColorImpl other)
        {
            return _borderBottomColor == other._borderBottomColor &&
                   Equals(_transition, other._transition);
            ;
        }
    }

    private class BorderLeftColorImpl : BaseModifier<BorderLeftColorImpl>
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

        protected override bool Equals(BorderLeftColorImpl other)
        {
            return _borderLeftColor == other._borderLeftColor &&
                   Equals(_transition, other._transition);
        }
    }

    private class BorderRightColorImpl : BaseModifier<BorderRightColorImpl>
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

        protected override bool Equals(BorderRightColorImpl other)
        {
            return _borderRightColor == other._borderRightColor &&
                   Equals(_transition, other._transition);
            ;
        }
    }

    public static IModifier BorderBottomLeftRadius(
        this IModifier style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderBottomLeftRadiusImpl(radius, transition));
    }

    public static IModifier BorderBottomRightRadius(
        this IModifier style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderBottomRightRadiusImpl(radius, transition));
    }

    public static IModifier BorderTopLeftRadius(
        this IModifier style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderTopLeftRadiusImpl(radius, transition));
    }

    public static IModifier BorderTopRightRadius(
        this IModifier style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderTopRightRadiusImpl(radius, transition));
    }

    public static IModifier BorderRadius(
        this IModifier style,
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

    public static IModifier BorderTopRadius(
        this IModifier style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderTopLeftRadiusImpl(radius, transition))
            .Then(new BorderTopRightRadiusImpl(radius, transition));
    }

    public static IModifier BorderBottomRadius(
        this IModifier style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderBottomLeftRadiusImpl(radius, transition))
            .Then(new BorderBottomRightRadiusImpl(radius, transition));
    }

    public static IModifier BorderLeftRadius(
        this IModifier style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderTopLeftRadiusImpl(radius, transition))
            .Then(new BorderBottomLeftRadiusImpl(radius, transition));
    }

    public static IModifier BorderRightRadius(
        this IModifier style,
        StyleLength radius,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderTopRightRadiusImpl(radius, transition))
            .Then(new BorderBottomRightRadiusImpl(radius, transition));
    }

    public static IModifier BorderTopWidth(
        this IModifier style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderTopWidthImpl(width, transition));
    }

    public static IModifier BorderBottomWidth(
        this IModifier style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderBottomWidthImpl(width, transition));
    }

    public static IModifier BorderLeftWidth(
        this IModifier style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderLeftWidthImpl(width, transition));
    }

    public static IModifier BorderRightWidth(
        this IModifier style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderRightWidthImpl(width, transition));
    }

    public static IModifier BorderWidth(
        this IModifier style,
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

    public static IModifier BorderWidth(
        this IModifier style,
        StyleFloat horizontalWidth,
        StyleFloat verticalWidth,
        ComposeTransition transition = default
    )
    {
        return style
            .BorderHorizontalWidth(horizontalWidth, transition)
            .BorderVerticalWidth(verticalWidth, transition);
    }

    public static IModifier BorderHorizontalWidth(
        this IModifier style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderLeftWidthImpl(width, transition))
            .Then(new BorderRightWidthImpl(width, transition));
    }

    public static IModifier BorderVerticalWidth(
        this IModifier style,
        StyleFloat width,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderTopWidthImpl(width, transition))
            .Then(new BorderBottomWidthImpl(width, transition));
    }

    public static IModifier BorderTopColor(
        this IModifier style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderTopColorImpl(color, transition));
    }

    public static IModifier BorderBottomColor(
        this IModifier style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderBottomColorImpl(color, transition));
    }

    public static IModifier BorderLeftColor(
        this IModifier style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderLeftColorImpl(color, transition));
    }

    public static IModifier BorderRightColor(
        this IModifier style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BorderRightColorImpl(color, transition));
    }

    public static IModifier BorderColor(
        this IModifier style,
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

    public static IModifier BorderColor(
        this IModifier style,
        StyleColor horizontalColor,
        StyleColor verticalColor,
        ComposeTransition transition = default
    )
    {
        return style
            .BorderHorizontalColor(horizontalColor, transition)
            .BorderVerticalColor(verticalColor, transition);
    }

    public static IModifier BorderHorizontalColor(
        this IModifier style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderLeftColorImpl(color, transition))
            .Then(new BorderRightColorImpl(color, transition));
    }

    public static IModifier BorderVerticalColor(
        this IModifier style,
        StyleColor color,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new BorderTopColorImpl(color, transition))
            .Then(new BorderBottomColorImpl(color, transition));
    }
}