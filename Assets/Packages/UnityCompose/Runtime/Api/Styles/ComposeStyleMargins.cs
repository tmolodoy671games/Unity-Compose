using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeStyleExtensions
{
    private class MarginTopImpl : ComposeStyle<MarginTopImpl>
    {
        private readonly StyleLength _marginTop;
        private readonly ComposeTransition _transition;

        public MarginTopImpl(StyleLength marginTop, ComposeTransition transition)
        {
            _marginTop = marginTop;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.marginTop = _marginTop;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "margin-top");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.MarginTop);
        }

        public override void Revert(VisualElement element)
        {
            element.style.marginTop = StyleKeyword.Null;
        }

        protected override bool Compare(MarginTopImpl other)
        {
            return _marginTop == other._marginTop && Equals(_transition, other._transition);
        }
    }

    private class MarginBottomImpl : ComposeStyle<MarginBottomImpl>
    {
        private readonly StyleLength _marginBottom;
        private readonly ComposeTransition _transition;

        public MarginBottomImpl(StyleLength marginBottom, ComposeTransition transition)
        {
            _marginBottom = marginBottom;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.marginBottom = _marginBottom;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "margin-bottom");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.MarginBottom);
        }

        public override void Revert(VisualElement element)
        {
            element.style.marginBottom = StyleKeyword.Null;
        }

        protected override bool Compare(MarginBottomImpl other)
        {
            return _marginBottom == other._marginBottom && Equals(_transition, other._transition);
        }
    }

    private class MarginLeftImpl : ComposeStyle<MarginLeftImpl>
    {
        private readonly StyleLength _marginLeft;
        private readonly ComposeTransition _transition;

        public MarginLeftImpl(StyleLength marginLeft, ComposeTransition transition)
        {
            _marginLeft = marginLeft;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.marginLeft = _marginLeft;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "margin-left");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.MarginLeft);
        }

        public override void Revert(VisualElement element)
        {
            element.style.marginLeft = StyleKeyword.Null;
        }

        protected override bool Compare(MarginLeftImpl other)
        {
            return _marginLeft == other._marginLeft && Equals(_transition, other._transition);
        }
    }

    private class MarginRightImpl : ComposeStyle<MarginRightImpl>
    {
        private readonly StyleLength _marginRight;
        private readonly ComposeTransition _transition;

        public MarginRightImpl(StyleLength marginRight, ComposeTransition transition)
        {
            _marginRight = marginRight;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.marginRight = _marginRight;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "margin-right");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.MarginRight);
        }

        public override void Revert(VisualElement element)
        {
            element.style.marginRight = StyleKeyword.Null;
        }

        protected override bool Compare(MarginRightImpl other)
        {
            return _marginRight == other._marginRight && Equals(_transition, other._transition);
        }
    }

    public static ComposeStyle MarginTop(
        this ComposeStyle style,
        StyleLength margin,
        ComposeTransition transition = default
    )
    {
        return style.Then(new MarginTopImpl(margin, transition));
    }

    public static ComposeStyle MarginBottom(
        this ComposeStyle style,
        StyleLength margin,
        ComposeTransition transition = default
    )
    {
        return style.Then(new MarginBottomImpl(margin, transition));
    }

    public static ComposeStyle MarginLeft(
        this ComposeStyle style,
        StyleLength margin,
        ComposeTransition transition = default
    )
    {
        return style.Then(new MarginLeftImpl(margin, transition));
    }

    public static ComposeStyle MarginRight(
        this ComposeStyle style,
        StyleLength margin,
        ComposeTransition transition = default
    )
    {
        return style.Then(new MarginRightImpl(margin, transition));
    }

    public static ComposeStyle MarginHorizontal(
        this ComposeStyle style,
        StyleLength margin,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new MarginLeftImpl(margin, transition))
            .Then(new MarginRightImpl(margin, transition));
    }

    public static ComposeStyle MarginVertical(
        this ComposeStyle style,
        StyleLength margin,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new MarginTopImpl(margin, transition))
            .Then(new MarginBottomImpl(margin, transition));
    }

    public static ComposeStyle Margin(
        this ComposeStyle style,
        StyleLength margin,
        ComposeTransition transition = default
    )
    {
        return style
            .MarginHorizontal(margin, transition)
            .MarginVertical(margin, transition);
    }

    public static ComposeStyle Margin(
        this ComposeStyle style,
        StyleLength horizontal,
        StyleLength vertical,
        ComposeTransition transition = default
    )
    {
        return style
            .MarginHorizontal(horizontal, transition)
            .MarginVertical(vertical, transition);
    }
}