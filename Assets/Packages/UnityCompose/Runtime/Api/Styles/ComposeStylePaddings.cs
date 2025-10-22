using StableCollections;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeStyleExtensions
{
    private class PaddingTopImpl : ComposeStyle<PaddingTopImpl>
    {
        private readonly StyleLength _paddingTop;
        private readonly ComposeTransition _transition;

        public PaddingTopImpl(StyleLength paddingTop, ComposeTransition transition)
        {
            _paddingTop = paddingTop;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.paddingTop = _paddingTop;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "padding-top");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.PaddingTop);
        }

        public override void Revert(VisualElement element)
        {
            element.style.paddingTop = StyleKeyword.Null;
        }

        protected override bool Compare(PaddingTopImpl other)
        {
            return _paddingTop == other._paddingTop && Equals(_transition, other._transition);
        }
    }

    private class PaddingBottomImpl : ComposeStyle<PaddingBottomImpl>
    {
        private readonly StyleLength _paddingBottom;
        private readonly ComposeTransition _transition;

        public PaddingBottomImpl(StyleLength paddingBottom, ComposeTransition transition)
        {
            _paddingBottom = paddingBottom;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.paddingBottom = _paddingBottom;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "padding-bottom");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.PaddingBottom);
        }

        public override void Revert(VisualElement element)
        {
            element.style.paddingBottom = StyleKeyword.Null;
        }

        protected override bool Compare(PaddingBottomImpl other)
        {
            return _paddingBottom == other._paddingBottom && Equals(_transition, other._transition);
        }
    }

    private class PaddingLeftImpl : ComposeStyle<PaddingLeftImpl>
    {
        private readonly StyleLength _paddingLeft;
        private readonly ComposeTransition _transition;

        public PaddingLeftImpl(StyleLength paddingLeft, ComposeTransition transition)
        {
            _paddingLeft = paddingLeft;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.paddingLeft = _paddingLeft;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "padding-left");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.PaddingLeft);
        }

        public override void Revert(VisualElement element)
        {
            element.style.paddingLeft = StyleKeyword.Null;
        }

        protected override bool Compare(PaddingLeftImpl other)
        {
            return _paddingLeft == other._paddingLeft && Equals(_transition, other._transition);
        }
    }

    private class PaddingRightImpl : ComposeStyle<PaddingRightImpl>
    {
        private readonly StyleLength _paddingRight;
        private readonly ComposeTransition _transition;

        public PaddingRightImpl(StyleLength paddingRight, ComposeTransition transition)
        {
            _paddingRight = paddingRight;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.paddingRight = _paddingRight;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "padding-right");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.PaddingRight);
        }

        public override void Revert(VisualElement element)
        {
            element.style.paddingRight = StyleKeyword.Null;
        }

        protected override bool Compare(PaddingRightImpl other)
        {
            return _paddingRight == other._paddingRight;
        }
    }

    public static ComposeStyle PaddingLeft(
        this ComposeStyle style,
        StyleLength padding,
        ComposeTransition transition = default
    )
    {
        return style.Then(new PaddingLeftImpl(padding, transition));
    }

    public static ComposeStyle PaddingRight(
        this ComposeStyle style,
        StyleLength padding,
        ComposeTransition transition = default
    )
    {
        return style.Then(new PaddingRightImpl(padding, transition));
    }

    public static ComposeStyle PaddingTop(
        this ComposeStyle style,
        StyleLength padding,
        ComposeTransition transition = default
    )
    {
        return style.Then(new PaddingTopImpl(padding, transition));
    }

    public static ComposeStyle PaddingBottom(
        this ComposeStyle style,
        StyleLength padding,
        ComposeTransition transition = default
    )
    {
        return style.Then(new PaddingBottomImpl(padding, transition));
    }

    public static ComposeStyle PaddingHorizontal(
        this ComposeStyle style,
        StyleLength padding,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new PaddingLeftImpl(padding, transition))
            .Then(new PaddingRightImpl(padding, transition));
    }

    public static ComposeStyle PaddingVertical(
        this ComposeStyle style,
        StyleLength padding,
        ComposeTransition transition = default
    )
    {
        return style
            .Then(new PaddingTopImpl(padding, transition))
            .Then(new PaddingBottomImpl(padding, transition));
    }

    public static ComposeStyle Padding(
        this ComposeStyle style,
        StyleLength padding,
        ComposeTransition transition = default
    )
    {
        return style
            .PaddingHorizontal(padding, transition)
            .PaddingVertical(padding, transition);
    }

    public static ComposeStyle Padding(
        this ComposeStyle style,
        StyleLength horizontal,
        StyleLength vertical,
        ComposeTransition transition = default
    )
    {
        return style
            .PaddingHorizontal(horizontal, transition)
            .PaddingVertical(vertical, transition);
    }
}