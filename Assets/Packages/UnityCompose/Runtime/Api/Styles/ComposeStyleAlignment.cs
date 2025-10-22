using StableCollections;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable CheckNamespace
namespace UnityCompose;

public static partial class ComposeStyleExtensions
{
    private class AlignSelfImpl : ComposeStyle<AlignSelfImpl>
    {
        private readonly StyleEnum<Align> _alignSelf;

        public AlignSelfImpl(StyleEnum<Align> alignSelf)
        {
            _alignSelf = alignSelf;
        }

        public override void Apply(VisualElement element)
        {
            element.style.alignSelf = _alignSelf;
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.AlignSelf);
        }

        public override void Revert(VisualElement element)
        {
            element.style.alignSelf = StyleKeyword.Null;
        }

        protected override bool Compare(AlignSelfImpl other)
        {
            return _alignSelf == other._alignSelf;
        }
    }

    private class FlexGrowImpl : ComposeStyle<FlexGrowImpl>
    {
        private readonly StyleFloat _flexGrow;
        private readonly ComposeTransition _transition;

        public FlexGrowImpl(StyleFloat flexGrow, ComposeTransition transition)
        {
            _flexGrow = flexGrow;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.flexGrow = _flexGrow;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "flex-grow");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.FlexGrow);
        }

        public override void Revert(VisualElement element)
        {
            element.style.flexGrow = StyleKeyword.Null;
        }

        protected override bool Compare(FlexGrowImpl other)
        {
            return _flexGrow == other._flexGrow;
        }
    }
        
    private class FlexShrinkImpl : ComposeStyle<FlexShrinkImpl>
    {
        private readonly StyleFloat _flexShrink;
        private readonly ComposeTransition _transition;

        public FlexShrinkImpl(StyleFloat flexShrink, ComposeTransition transition)
        {
            _flexShrink = flexShrink;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.flexShrink = _flexShrink;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "flex-shrink");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.FlexShrink);
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(FlexShrinkImpl other)
        {
            return _flexShrink.Equals(other._flexShrink) && Equals(_transition, other._transition);
        }
    }

    private class PositionImpl : ComposeStyle<PositionImpl>
    {
        private readonly StyleEnum<Position> _position;

        public PositionImpl(StyleEnum<Position> position)
        {
            _position = position;
        }

        public override void Apply(VisualElement element)
        {
            element.style.position = _position;
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Position);
        }

        public override void Revert(VisualElement element)
        {
            element.style.position = StyleKeyword.Null;
        }

        protected override bool Compare(PositionImpl other)
        {
            return _position == other._position;
        }
    }

    private class TopImpl : ComposeStyle<TopImpl>
    {
        private readonly StyleLength _top;
        private readonly ComposeTransition _transition;

        public TopImpl(StyleLength top, ComposeTransition transition)
        {
            _top = top;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.top = _top;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "top");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Top);
        }

        public override void Revert(VisualElement element)
        {
            element.style.top = StyleKeyword.Null;
        }

        protected override bool Compare(TopImpl other)
        {
            return _top == other._top && Equals(_transition, other._transition);
        }
    }

    private class BottomImpl : ComposeStyle<BottomImpl>
    {
        private readonly StyleLength _bottom;
        private readonly ComposeTransition _transition;

        public BottomImpl(StyleLength bottom, ComposeTransition transition)
        {
            _bottom = bottom;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.bottom = _bottom;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "bottom");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Bottom);
        }

        public override void Revert(VisualElement element)
        {
            element.style.bottom = StyleKeyword.Null;
        }

        protected override bool Compare(BottomImpl other)
        {
            return _bottom == other._bottom && Equals(_transition, other._transition);
        }
    }

    private class LeftImpl : ComposeStyle<LeftImpl>
    {
        private readonly StyleLength _left;
        private readonly ComposeTransition _transition;

        public LeftImpl(StyleLength left, ComposeTransition transition)
        {
            _left = left;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.left = _left;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "left");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Left);
        }

        public override void Revert(VisualElement element)
        {
            element.style.left = StyleKeyword.Null;
        }

        protected override bool Compare(LeftImpl other)
        {
            return _left == other._left && Equals(_transition, other._transition);
        }
    }

    private class RightImpl : ComposeStyle<RightImpl>
    {
        private readonly StyleLength _right;
        private readonly ComposeTransition _transition;

        public RightImpl(StyleLength right, ComposeTransition transition)
        {
            _right = right;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.right = _right;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "right");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Right);
        }

        public override void Revert(VisualElement element)
        {
            element.style.right = StyleKeyword.Null;
        }

        protected override bool Compare(RightImpl other)
        {
            return _right == other._right && Equals(_transition, other._transition);
            ;
        }
    }

    private class TranslateImpl : ComposeStyle<TranslateImpl>
    {
        private readonly StyleTranslate _translate;
        private readonly ComposeTransition _transition;

        public TranslateImpl(StyleTranslate translate, ComposeTransition transition)
        {
            _transition = transition;
            _translate = translate;
        }

        public override void Apply(VisualElement element)
        {
            element.style.translate = _translate;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "translate");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.Translate);
        }

        public override void Revert(VisualElement element)
        {
            element.style.translate = StyleKeyword.Null;
        }

        protected override bool Compare(TranslateImpl other)
        {
            return _translate == other._translate && Equals(_transition, other._transition);
        }
    }

    private class TransformOriginImpl : ComposeStyle<TransformOriginImpl>
    {
        private readonly TransformOrigin _origin;
        private readonly ComposeTransition _transition;

        public TransformOriginImpl(TransformOrigin origin, ComposeTransition transition)
        {
            _origin = origin;
            _transition = transition;
        }

        public override void Apply(VisualElement element)
        {
            element.style.transformOrigin = _origin;
            if (!_transition.IsDefault())
                element.AddTransition(_transition, "transform-origin");
        }

        public override void Apply(IMutableStableSet<ComposeModifiedProperty> modifiedProperties)
        {
            modifiedProperties.Add(ComposeModifiedProperty.TransformOrigin);
        }

        public override void Revert(VisualElement element)
        {
        }

        protected override bool Compare(TransformOriginImpl other)
        {
            return _origin.Equals(other._origin) && Equals(_transition, other._transition);
        }
    }

    public static ComposeStyle AlignSelf(this ComposeStyle style, StyleEnum<Align> alignSelf)
    {
        return style.Then(new AlignSelfImpl(alignSelf));
    }

    public static ComposeStyle FlexGrow(
        this ComposeStyle style,
        StyleFloat flexGrow,
        ComposeTransition transition = default
    )
    {
        return style.Then(new FlexGrowImpl(flexGrow, transition));
    }

    public static ComposeStyle FlexShrink(
        this ComposeStyle style,
        StyleFloat flexShrink,
        ComposeTransition transition = default
    )
    {
        return style.Then(new FlexShrinkImpl(flexShrink, transition));
    }

    public static ComposeStyle Position(this ComposeStyle style, StyleEnum<Position> position)
    {
        return style.Then(new PositionImpl(position));
    }

    public static ComposeStyle Top(
        this ComposeStyle style,
        StyleLength top,
        ComposeTransition transition = default
    )
    {
        return style.Then(new TopImpl(top, transition));
    }

    public static ComposeStyle Bottom(
        this ComposeStyle style,
        StyleLength bottom,
        ComposeTransition transition = default
    )
    {
        return style.Then(new BottomImpl(bottom, transition));
    }

    public static ComposeStyle Left(
        this ComposeStyle style,
        StyleLength left,
        ComposeTransition transition = default
    )
    {
        return style.Then(new LeftImpl(left, transition));
    }

    public static ComposeStyle Right(
        this ComposeStyle style,
        StyleLength right,
        ComposeTransition transition = default
    )
    {
        return style.Then(new RightImpl(right, transition));
    }

    public static ComposeStyle Translate(
        this ComposeStyle style,
        StyleTranslate translate,
        ComposeTransition transition = default
    )
    {
        return style.Then(new TranslateImpl(translate, transition));
    }

    public static ComposeStyle Translate(
        this ComposeStyle style,
        Length x = default,
        Length y = default,
        ComposeTransition transition = default
    )
    {
        return style.Then(new TranslateImpl(new Translate(x, y), transition));
    }

    public static ComposeStyle Pivot(
        this ComposeStyle style,
        Vector2 translate,
        ComposeTransition transition = default
    )
    {
        return style.Then(
            new TranslateImpl(
                new Translate(
                    new Length(-translate.x * 100, LengthUnit.Percent),
                    new Length(-translate.y * 100, LengthUnit.Percent)
                ),
                transition
            )
        );
    }

    public static ComposeStyle Pivot(
        this ComposeStyle style,
        float left,
        float top,
        ComposeTransition transition = default
    )
    {
        return style.Pivot(new Vector2(left, top), transition);
    }

    public static ComposeStyle TransformOrigin(
        this ComposeStyle style,
        TransformOrigin origin,
        ComposeTransition transition = default
    )
    {
        return style.Then(new TransformOriginImpl(origin, transition));
    }

    public static ComposeStyle TransformOrigin(
        this ComposeStyle style,
        float left = 0.5f,
        float top = 0.5f,
        ComposeTransition transition = default
    )
    {
        return style.Then(
            new TransformOriginImpl(
                new TransformOrigin((left * -100).Percent(), (top * -100).Percent()),
                transition
            )
        );
    }
}